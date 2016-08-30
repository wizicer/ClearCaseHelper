namespace IcerDesign.CCHelper
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Windows.Forms;

    public partial class frmCommitter : Form
    {
        private static log4net.ILog mainLog = log4net.LogManager.GetLogger("main");

        private readonly LocationInfo locInfo;
        private readonly string configSpecFilePath;
        private readonly string labelPattern;

        public frmCommitter(LocationInfo locationInfo, string configSpecFilePath, string labelPattern)
        {
            this.locInfo = locationInfo;
            this.configSpecFilePath = configSpecFilePath;
            this.labelPattern = labelPattern;
            this.InitializeComponent();
        }

        private bool CheckOutFileUntilUserGiveUp(string comment, string filename)
        {
            var fileNotCheckOut = true;
            while (fileNotCheckOut)
            {
                try
                {
                    locInfo.CheckOutFile(comment, filename);
                    fileNotCheckOut = false;
                }
                catch (Exception ex)
                {
                    var ret = MessageBox.Show(
                        string.Format("Check out {0} failed, error:{1}\nDo you want to retry?", filename, ex.Message),
                        "failed",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    if (ret == DialogResult.No) break;
                }
            }
            return !fileNotCheckOut;
        }
        private void btnRefreshCommit_Click(object sender, EventArgs e)
        {
            RefreshCommit();
        }

        private void RefreshCommit()
        {
            this.btnRefreshCommit.Enabled = false;
            this.btnRefreshCommit.Text = "Refreshing...";
            Application.DoEvents();

            // make sure the config-spec is the latest one
            var cs = locInfo.GetConfigSpecOfView();
            var cs2 = locInfo.GetConfigSpecOfFile(configSpecFilePath);

            cs2 = string.Format("{1}{0}{2}{0}{3}{0}", Environment.NewLine, "element * CHECKEDOUT", cs2, @"element * ...\main\LATEST");

            Func<string, string> cleanConfigSpec = (s) => s.Trim().Replace(" ", "").Replace("\r", "").Replace("\n", "");

            if (cleanConfigSpec(cs) != cleanConfigSpec(cs2))
            {
                MessageBox.Show("the config spec isn't the latest one, please update config spec first!");
                btnRefreshCommit.Text = "Refresh";
                this.btnRefreshCommit.Enabled = true;
                return;
            }

            // generate the newest label
            this.btnRefreshCommit.Text = "Getting Label...";
            Application.DoEvents();

            string pattern = @"element \* (?<label>" + labelPattern + ")";
            var regex = new Regex(pattern, RegexOptions.None);
            var prefixLabel = regex.Match(cs).Groups["label"].Value;
            if (string.IsNullOrEmpty(prefixLabel))
            {
                MessageBox.Show(string.Format("the target label[{0}] is not found in the configspec.txt file, you may using wrong profile", labelPattern));
                this.btnRefreshCommit.Text = "Refresh";
                this.btnRefreshCommit.Enabled = true;
                return;
            }

            //var r2 = new Regex(@"element \* " + prefixLabel.Replace(".", @"\.") + @"_(?<num>\d+)", RegexOptions.None);
            //var num = Convert.ToInt32(r2.Match(cs).Groups["num"].Value);

            // 2nd method to get latest label, we are using this one
            var ret = locInfo.GetLabels(prefixLabel)
                .Select(s => s.Replace(prefixLabel + "_", ""))
                .Select(s => { int c; int.TryParse(s, out c); return c; })
                .ToArray();

            if (!ret.Any())
            {
                this.lblLabelToCommit.Text = "Can't find the label!";
                btnRefreshCommit.Text = "Refresh";
                this.btnRefreshCommit.Enabled = true;
                return;
            }

            var num = ret.Max();

            var label = prefixLabel + "_" + (num + 1);
            this.lblLabelToCommit.Text = label;
            if (string.IsNullOrEmpty(this.txtAlternativeLabel.Text)) this.txtAlternativeLabel.Text = label;

            // list all checked out files
            this.btnRefreshCommit.Text = "Getting Checkedout File...";
            Application.DoEvents();

            var lsco = locInfo.GetCheckedOutFiles();
            lstChanges.Items.Clear();
            foreach (var item in lsco)
            {
                lstChanges.Items.Add(item);
            }

            // finding modifed file(there is bug in `cleartool diff` which will not compare binary file)
            if (!this.chkIgnoreModify.Checked)
            {
                this.btnRefreshCommit.Text = "Finding Modified File...";
                Application.DoEvents();
                var lschanged = locInfo.GetCheckedOutChangedFiles();
                foreach (var item in this.lstChanges.Items.OfType<ListViewItem>())
                {
                    var status = lschanged.Any(s => s == item.Text) ? "Modified" : "CheckedOut";
                    item.SubItems.Add(status);
                }
            }

            lstChanges.Enabled = true;
            txtCommitComment.Enabled = true;
            btnCommit.Enabled = true;
            btnRefreshCommit.Text = "Refresh";
            btnRefreshCommit.Enabled = true;
            lblLabelToCommit.ForeColor = SystemColors.ControlText;
        }

        private void btnCommit_Click(object sender, EventArgs e)
        {
            var comment = this.txtCommitComment.Text.Replace("\r", "").Replace("\n", "").Replace("\"", "'");
            var label = this.chkUseAlternativeLabel.Checked ? this.txtAlternativeLabel.Text : this.lblLabelToCommit.Text;

            if (string.IsNullOrEmpty(comment))
            {
                MessageBox.Show("please make sure the commit comment is not empty");
                return;
            }

            if (string.IsNullOrEmpty(label))
            {
                MessageBox.Show("the label can not be empty");
                return;
            }

            if (comment.IndexOf(":") == -1)
            {
                MessageBox.Show("please enter your name to comment with colon(:)\r\ne.g: Icer: remove PI reset variable");
                return;
            }

            var files = lstChanges.Items
                .OfType<ListViewItem>()
                .Where(lvt => lvt.Checked)
                .Select(lvt => lvt.Text)
                .ToArray();
            if (!files.Any())
            {
                MessageBox.Show("You do not check out any file.");
                return;
            }

            // reorder file to make sure the folder at last to check in to avoid directory scope problem in CC
            files = files
                .OrderByDescending(s => s.Length)
                .OrderBy(s => Directory.Exists("z:" + s) ? 1 : 0)
                .ToArray();

            var fileexample = string.Join(Environment.NewLine, files.Take(10));
            var ret = MessageBox.Show(
                string.Format("About to commit following files:\r\n{0}\r\n...\r\nTotal: {1} elements", fileexample, files.Count()),
                "Commit?",
                MessageBoxButtons.YesNo);
            if (ret != DialogResult.Yes)
            {
                return;
            }

            bool flag = true;

            // create label
            locInfo.CreateLabel(comment, label);

            // apply label
            var cmdApplyLabel = ClearCommands.ApplyLabelToFiles(label, files);
            flag = frmRunCommand.RunClearCommand(cmdApplyLabel, true, true);
            if (!flag) return;

            // check in files
            var cmdCheckIn = ClearCommands.CheckInFiles(comment, files);
            flag = frmRunCommand.RunClearCommand(cmdCheckIn, true, false);
            if (!flag) return;

            // write back config spec
            if (!this.chkDoNotModifyConfigSpecTxt.Checked)
            {
                flag = ModifyConfigSpec(label, comment);
                if (!flag) return;
            }

            // update config spec
            locInfo.UpdateConfigSpecMain(configSpecFilePath);

            // refresh
            this.RefreshCommit();
            this.txtCommitComment.Text = string.Empty;
            MessageBox.Show("Finished commit");
        }

        private bool ModifyConfigSpec(string label, string comment)
        {
            var filename = Path.Combine(locInfo.BasePath, configSpecFilePath);
            var checkedout = CheckOutFileUntilUserGiveUp(comment, filename);
            if (!checkedout)
            {
                MessageBox.Show("failed to update configspec.txt, please manually update the configspec.txt");
                return true;
            }

            var separator = @"#
# Add developer labels below this line
#
".Replace("\r\n", "\n");
            var devlabel = string.Format("element * {0} # {1}", label, comment);
            var cs = File.ReadAllText(filename).Replace("\r", "");
            if (cs.IndexOf(separator) == -1)
            {
                if (Debugger.IsAttached) Debugger.Break();
                MessageBox.Show("Indicator is not present in configspec.txt file, please check manually!");
                locInfo.UndoCheckOutFile(filename);
            }
            else
            {
                cs = cs.Replace(separator, string.Format("{0}{1}{2}", separator, "\n", devlabel));
                cs = cs.Replace("\n", "\r\n");
                File.WriteAllText(filename, cs);
                try
                {
                    locInfo.CheckInFile(comment, filename);
                }
                catch (ExecutionException ex)
                {
                    if (ex.Message.IndexOf("with data identical to predecessor") > -1)
                    {
                        MessageBox.Show("something wrong occurred, so undo checkout!");
                        locInfo.UndoCheckOutFile(filename);
                        return false;
                    }
                }
            }

            return true;
        }

        private void mnubtnRevert_Click(object sender, EventArgs e)
        {
            var items = lstChanges.SelectedItems.OfType<ListViewItem>().Select(lvt => lvt.Text).ToArray();
            var ret = MessageBox.Show(string.Format("About to revert following files:\r\n{0}", string.Join(Environment.NewLine, items)), "Revert?", MessageBoxButtons.YesNo);
            if (ret == System.Windows.Forms.DialogResult.Yes)
            {
                var cmds = ClearCommands.UndoCheckOut(items);
                frmRunCommand.RunClearCommand(cmds);
                RefreshCommit();
            }
        }

        private void lstChanges_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A && e.Control)
            {
                foreach (ListViewItem item in lstChanges.Items)
                {
                    item.Selected = true;
                }
            }
        }

        private void chkUseAlternativeLabel_CheckedChanged(object sender, EventArgs e)
        {
            this.txtAlternativeLabel.Enabled = this.chkUseAlternativeLabel.Checked;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
        }
    }
}
