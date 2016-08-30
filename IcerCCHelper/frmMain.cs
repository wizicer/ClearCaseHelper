namespace IcerDesign.CCHelper
{
    using Guide;
    using Server;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Windows.Forms;

    public partial class frmMain : Form
    {
        private static log4net.ILog mainLog = log4net.LogManager.GetLogger("main");

        private DateTime Deadline = new DateTime(2017, 6, 1);

        private int DaysAfterDeadline = 15;

        private GuideAction guideAction;

        public frmMain()
        {
            this.InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            if ((DateTime.Now - Deadline).TotalMilliseconds > 0 && (DateTime.Now - Deadline).TotalDays < DaysAfterDeadline)
            {
                MessageBox.Show("This version is going to out of date,\r\nPlease find Icer to get new version immediately.", "Out of date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if ((DateTime.Now - Deadline).TotalDays > DaysAfterDeadline)
            {
                MessageBox.Show("This version is out of date,\r\nPlease find Icer to get new version.", "Out of date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Environment.Exit(0);
            }

            this.lblVersion.Text = string.Format("Ver: {0}\r\nEvaluation To: {1}", Application.ProductVersion, Deadline.ToShortDateString());

            this.RefreshExtensionButton();
            this.InitServerList();
            this.InitGuide();
        }

        private void InitServerList()
        {
            var sm = new ServerManager();
            this.cmbServerList.Items.Clear();
            this.cmbServerList.Items.Add($"Local");
            foreach (var server in sm.ServerList)
            {
                this.cmbServerList.Items.Add($"{server.Name}[{server.URL}]");
            }
        }

        private void InitGuide()
        {
            this.guideAction = new GuideAction();
            GotoDefaultPage();
            HtmlElementEventHandler handler = (s, e) =>
            {
                var doc = s as HtmlDocument;
                if (doc.ActiveElement.TagName.ToLower() == "a" || doc.ActiveElement.TagName.ToLower() == "button")
                {
                    var action = doc.ActiveElement.GetAttribute("data-action");
                    if (!string.IsNullOrEmpty(action))
                    {
                        this.guideAction.Execute(action);
                    }
                }
            };

            this.webGuide.DocumentCompleted += (s, e) =>
            {
                var web = s as WebBrowser;
                var metas = web.Document.GetElementsByTagName("meta");
                var minver = metas
                    .OfType<HtmlElement>()
                    .FirstOrDefault(_ => _.GetAttribute("name") == "minicercchelperversion")
                    ?.GetAttribute("content");
                if (string.IsNullOrEmpty(minver))
                {
                    mainLog.Warn($"meta minicercchelperversion is not exist in [{web.Url}], navigate to default page.");
                    GotoDefaultPage();
                    return;
                }

                var result = new Version(minver).CompareTo(new Version(Application.ProductVersion));
                if(result > 0)
                {
                    MessageBox.Show($"Required minimum version of CC Helper is {minver}, this CC Helper is out of date, you should update to met the minimum requirement.");
                    mainLog.Warn($"minicercchelperversion is not met in [{web.Url}], navigate to default page.");
                    GotoDefaultPage();
                    return;
                }

                web.Document.InvokeScript("checkversion", new[] { Application.ProductVersion });

                web.Document.Click += handler;
            };
            this.webGuide.Navigated += (s, e) =>
            {
                var web = s as WebBrowser;
                web.Document.Click -= handler;
            };
        }

        private void GotoDefaultPage()
        {
            var file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "guidepage.html");
            this.webGuide.Navigate($"file:///{file}");
        }

        private void RefreshExtensionButton()
        {
            switch (Installer.GetInstallStatus())
            {
                case Installer.InstallStatus.Newest:
                    this.btnRegisterExtension.Text = "Uninstall Extension";
                    break;

                case Installer.InstallStatus.Outdated:
                    this.btnRegisterExtension.Text = "Upgrade Extension";
                    var dlgRet = MessageBox.Show("Your extension is out of date, do you want to upgrade?", "Upgrade?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (dlgRet == System.Windows.Forms.DialogResult.OK)
                    {
                        this.RegisterExtension();
                    }

                    break;

                case Installer.InstallStatus.Future:
                    this.btnRegisterExtension.Text = "Your Helper is outdated";
                    this.btnRegisterExtension.Enabled = false;
                    var ret = MessageBox.Show("Your Helper is out of date, please switch to the right version of CC Helper.", "Out of date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                    break;

                case Installer.InstallStatus.None:
                default:
                    this.btnRegisterExtension.Text = "Install Extension";
                    break;
            }
        }

        private void btnRegisterExtension_Click(object sender, EventArgs e)
        {
            this.RegisterExtension();
        }

        private void RegisterExtension()
        {
            var adminErrorMsg = "You have to click 'Yes' to install this extension properly.";

            try
            {
                var executablePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.FriendlyName);
                switch (Installer.GetInstallStatus())
                {
                    case Installer.InstallStatus.Newest:
                        Installer.Uninstall();
                        break;

                    case Installer.InstallStatus.Outdated:
                        Installer.Uninstall();
                        Installer.Install(executablePath);
                        break;

                    case Installer.InstallStatus.None:
                        Installer.Install(executablePath);
                        break;

                    case Installer.InstallStatus.Future:
                    default:
                        throw new NotImplementedException();
                }
            }
            catch (Win32Exception ex)
            {
                if (ex.NativeErrorCode != 1223) throw;
                MessageBox.Show(adminErrorMsg);
                return;
            }
            this.RefreshExtensionButton();
        }

        private void cmbServerList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var s = this.cmbServerList.SelectedItem as string;
            var rx = new Regex(@".*\[(?<url>.*)\]");
            var m = rx.Match(s);
            if (m.Success)
            {
                var url = m.Groups["url"].Value;
                this.webGuide.Navigate(url);
            }
            else
            {
                this.GotoDefaultPage();
            }
        }
    }
}
