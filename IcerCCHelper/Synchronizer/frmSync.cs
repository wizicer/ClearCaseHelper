namespace IcerDesign.CCHelper
{
    using Properties;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Xml;
    using System.Xml.Serialization;

    public partial class frmSync : Form
    {
        public frmSync()
        {
            InitializeComponent();
        }

        private void frmSync_Load(object sender, EventArgs e)
        {
            CheckGit();
            InitSession();
        }

        private void CheckGit()
        {
            try
            {
                var ret = DAL.RunBatch("@git --version");
                lnklblCheckError.Visible = false;
            }
            catch (ExecutionException)
            {
                lnklblCheckError.Text = "git executable not found. click to know how to proceed.";
                foreach (Control ctrl in this.Controls)
                {
                    ctrl.Enabled = false;
                }

                lnklblCheckError.Enabled = true;
            }
        }

        private void InitSession()
        {
            var session = DeserializeObject<CCHelperProfileSession>(Settings.Default.SyncLastSession) ?? new CCHelperProfileSession();
            this.LoadSession(session);
            this.LoadProfile(Settings.Default.SyncLastLoadedProfile);
            this.txtSourceBranch.TextChanged += SessionChanged;
            this.txtSourceUrl.TextChanged += SessionChanged;
            this.txtSourceBasePath.TextChanged += SessionChanged;
            this.txtSourceRule.TextChanged += SessionChanged;
            this.txtSourceUsername.TextChanged += SessionChanged;
            this.txtSourcePassword.TextChanged += SessionChanged;
            this.txtDestPath.TextChanged += SessionChanged;
            this.txtDestRule.TextChanged += SessionChanged;
            this.chkDestSameAsSource.CheckedChanged += SessionChanged;
        }

        private void LoadSession(CCHelperProfileSession session)
        {
            this.txtSourceBranch.Text = session.SourceBranch ?? "master";
            this.txtSourceUrl.Text = session.SourceURL ?? "";
            this.txtSourceBasePath.Text = session.SourceBasePath ?? @"vob";
            this.txtSourceRule.Text = StandardizeLineEnd(session.SourceFilter ?? @"");
            this.txtSourceUsername.Text = session.SourceUsername ?? @"";
            this.txtSourcePassword.Text = session.SourcePassword ?? @"";
            this.txtDestPath.Text = session.DestPath ?? @"z:\";
            this.txtDestRule.Text = StandardizeLineEnd(session.DestFilter ?? @"");
            this.chkDestSameAsSource.Checked = session.FilterDestSameAsSourceSpecified ? session.FilterDestSameAsSource : true;
        }

        private void SaveSession()
        {
            var content = SerializeObject(this.GetSessionDataFromUI());
            Settings.Default.SyncLastSession = content;
            Settings.Default.Save();
        }

        private T DeserializeObject<T>(string content) where T : class
        {
            try
            {
                using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(content)))
                {
                    var xs = new XmlSerializer(typeof(T));
                    var obj = xs.Deserialize(stream) as T;
                    return obj;
                }
            }
            catch (InvalidOperationException ex) when (ex.InnerException is XmlException)
            {
                return null;
            }
        }

        private string SerializeObject<T>(T @object)
        {
            using (var stream = new MemoryStream())
            {
                var xs = new XmlSerializer(typeof(T));
                xs.Serialize(stream, @object);
                stream.Seek(0, SeekOrigin.Begin);
                using (var sr = new StreamReader(stream, Encoding.UTF8))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        private CCHelperProfileSession GetSessionDataFromUI()
        {
            var session = new CCHelperProfileSession();
            session.DestPath = this.txtDestPath.Text;
            session.DestFilter = this.txtDestRule.Text;
            session.FilterDestSameAsSource = this.chkDestSameAsSource.Checked;
            session.FilterDestSameAsSourceSpecified = true;
            session.SourceURL = this.txtSourceUrl.Text;
            session.SourceBasePath = this.txtSourceBasePath.Text;
            session.SourceBranch = this.txtSourceBranch.Text;
            session.SourceFilter = this.txtSourceRule.Text;
            session.SourceUsername = this.txtSourceUsername.Text;
            session.SourcePassword = this.txtSourcePassword.Text;
            return session;
        }

        private void SessionChanged(object sender, EventArgs e)
        {
            this.SaveSession();
        }

        private async void btnCompare_Click(object sender, EventArgs e)
        {
            var ss = this.GetSessionDataFromUI();
            if (ss.SourceURL.ToLower().StartsWith("http") && (string.IsNullOrEmpty(ss.SourceUsername) || string.IsNullOrEmpty(ss.SourcePassword)))
            {
                this.ReportStatus("user or password cannot be empty for http(s) url.");
                return;
            }

            ss.DestFilter = ss.FilterDestSameAsSource ? ss.SourceFilter : ss.DestFilter;
            var reponame = GetRepoName(ss.SourceURL);
            var tempdir = Path.Combine(Path.GetTempPath(), $@"icercchelper\git\{reponame}");

            lstChanges.Items.Clear();
            this.ReportStatus("updating git source");
            var url = GetAuthenticateURL(ss.SourceURL, ss.SourceUsername, ss.SourcePassword);
            if (url == null)
            {
                this.ReportStatus("update failed due to wrong source url");
                return;
            }

            await CloneOrUpdateToSpecificBranchLatest(url, ss.SourceBranch, tempdir);
            this.ReportStatus("updated git source");

            var workdir = Path.Combine(tempdir, ss.SourceBasePath);
            var ls = await new FolderComparer().CompareAsync(
                workdir,
                RuleHelper.GenRules(ss.SourceFilter).ToArray(),
                ss.DestPath,
                RuleHelper.GenRules(ss.DestFilter).ToArray(),
                new Progress<WorkProgress>((_) => this.ReportStatus((_.Progress + _.Total == 0 ? "" : $"[{_.Progress}/{_.Total}]") + $"{_.StepDetail}")));

            this.ReportStatus("updating list");
            lstChanges.Tag = ls;
            lstChanges.BeginUpdate();
            foreach (var item in ls)
            {
                var it = lstChanges.Items.Add(item.Path);
                it.SubItems.Add(item.Result.ToString());
            }
            lstChanges.EndUpdate();
            Application.DoEvents();

            // empty status after finish
            this.ReportStatus(string.Empty);
        }

        private string GetAuthenticateURL(string url, string username, string password)
        {
            var r = new Regex("(?<first>https?://)(.*@)?(?<second>.*)");
            var m = r.Match(url);
            if (!m.Success) return null;

            var newurl = $"{m.Groups["first"]}{username}:{password}@{m.Groups["second"]}";
            return newurl;
        }

        private void ReportStatus(string message)
        {
            lblProgress.Text = message;
            Application.DoEvents();
        }

        private static Task CloneOrUpdateToSpecificBranchLatest(string url, string branch, string tempdir)
        {
            if (Directory.Exists(tempdir))
            {
                return RunBatchAsync($@"cd /d {tempdir}
git fetch origin {branch}:refs/remotes/origin/{branch}
git checkout FETCH_HEAD
git reset --hard
git clean -fdx");
            }
            else
            {
                return RunBatchAsync($@"git clone {url} --branch {branch} --single-branch ""{tempdir}""");
            }
        }

        private static Task RunBatchAsync(string script)
        {
            return Task.Run(() => DAL.RunBatch(script));
        }

        private object GetRepoName(string url)
        {
            var lastpart = url.Substring(url.LastIndexOf("/") + 1);
            return lastpart;
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            var comment = "";
            var list = lstChanges.Tag as ElementCompareResult[];
            if (list == null) return;

            var commands = new List<CommandBase>();

            var listDelete = list.Where(_ => _.Result == CompareResult.Delete).ToArray();
            foreach (var item in listDelete)
            {
                commands.AddRange(ClearCommands.RemoveFromSourceControl(item.DestPath));
            }

            var listUpdate = list.Where(_ => _.Result == CompareResult.Update).ToArray();
            foreach (var item in listUpdate)
            {
                commands.AddRange(ClearCommands.CheckOutFile(comment, item.DestPath));
                commands.Add(new ShellCommand($@"copy /y ""{item.SourcePath}"" ""{item.DestPath}""", null));
            }

            var listCreate = list.Where(_ => _.Result == CompareResult.Create).ToArray();
            foreach (var item in listCreate)
            {
                if (item.Type == ElementType.File)
                {
                    commands.Add(new ShellCommand($@"copy /y ""{item.SourcePath}"" ""{item.DestPath}""", null));
                }
                else
                {
                    commands.Add(new ShellCommand($@"mkdir ""{item.DestPath}""", null));
                }

                var parent = Directory.GetParent(item.DestPath).FullName;
                commands.AddRange(ClearCommands.CheckOutFile("", parent));
                commands.Add(new ClearCommand($"mkelem -mkpath -master -nc \"{item.DestPath}\"", parent));
            }

            frmRunCommand.RunClearCommand(commands.ToArray());
        }

        private void btnLoadSavedSessions_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "Session File (*.xml)|*.xml";
            var result = dlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                var file = dlg.FileName;

                LoadProfile(file);
            }
        }

        private void LoadProfile(string file)
        {
            if (File.Exists(file))
            {
                using (var stream = File.Open(file, FileMode.Open))
                {
                    var xs = new XmlSerializer(typeof(CCHelperProfile));
                    var obj = xs.Deserialize(stream) as CCHelperProfile;
                    this.loadedProfile = obj;
                    cmbSessions.Items.Clear();
                    foreach (var session in this.loadedProfile.Sessions)
                    {
                        cmbSessions.Items.Add(session.Name);
                    }
                }

                lblLoadedProfile.Text = file;
                Settings.Default.SyncLastLoadedProfile = file;
                Settings.Default.Save();
            }
        }

        private string StandardizeLineEnd(string input)
        {
            Regex r = new Regex("(?<!\r)\n");
            return r.Replace(input, "\r\n");
        }

        private CCHelperProfile loadedProfile;

        private void cmbSessions_SelectedIndexChanged(object sender, EventArgs e)
        {
            var session = this.loadedProfile.Sessions.First(_ => _.Name == cmbSessions.SelectedItem.ToString());
            session.DestFilter = StandardizeLineEnd(session.DestFilter);
            session.SourceFilter = StandardizeLineEnd(session.SourceFilter);
            this.LoadSession(session);
        }

        private void chkDestSameAsSource_CheckedChanged(object sender, EventArgs e)
        {
            txtDestRule.Enabled = !chkDestSameAsSource.Checked;
        }

        private void lnklblCheckError_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://git-scm.com/");
        }

        private void txtSourceUrl_TextChanged(object sender, EventArgs e)
        {
            if (txtSourceUrl.Text.ToLower().StartsWith("http"))
            {
                txtSourcePassword.Enabled = true;
                txtSourceUsername.Enabled = true;
            }
            else
            {
                txtSourcePassword.Enabled = false;
                txtSourceUsername.Enabled = false;
                txtSourcePassword.Text = null;
                txtSourceUsername.Text = null;
            }
        }
    }
}