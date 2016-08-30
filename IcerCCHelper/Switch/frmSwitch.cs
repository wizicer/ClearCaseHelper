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
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public partial class frmSwitch : Form
    {
        private static log4net.ILog mainLog = log4net.LogManager.GetLogger("main");

        private LocationInfo locInfo;

        private readonly string configSpecFilePath;

        public frmSwitch(LocationInfo locationInfo, string configSpecFilePath, SwitchType defaultSwitchType = SwitchType.None)
        {
            this.locInfo = locationInfo;
            this.configSpecFilePath = configSpecFilePath;
            this.InitializeComponent();
            switch (defaultSwitchType)
            {
                case SwitchType.Branch:
                    this.radBranch.Checked = true;
                    break;
                case SwitchType.DeveloperMain:
                    this.radDeveloperMain.Checked = true;
                    break;
                case SwitchType.VerifiableMain:
                    this.radVerifiableMain.Checked = true;
                    break;
                case SwitchType.None:
                default:
                    break;
            }
        }

        public enum SwitchType
        {
            None,
            Branch,
            DeveloperMain,
            VerifiableMain,
        }

        private bool IsBranchExist(string branch)
        {
            try
            {
                return locInfo.IsBranchExist(branch);
            }
            catch (Exception ex)
            {
                mainLog.Warn("failed to find the branch", ex);
            }

            return false;
        }

        private void CreateBranch(string branch)
        {
            try
            {
                locInfo.CreateBranch(branch);
            }
            catch (Exception ex)
            {
                mainLog.Warn("failed to create branch", ex);
            }
        }

        private void btnInspect_Click(object sender, EventArgs e)
        {
            MessageBox.Show(locInfo.GetConfigSpecOfView());
        }

        private async void frmSwitch_Load(object sender, EventArgs e)
        {
            var path = Path.Combine(this.locInfo.BasePath, this.configSpecFilePath);
            if (!File.Exists(path))
            {
                MessageBox.Show($"File [{path}] cannot find, make sure you used right vob and mounted correctly");
                this.Close();
                return;
            }

            this.UpdateBranchStatus();
            await this.RefreshBranchesAsync();
        }

        private void UpdateBranchStatus()
        {
            this.lblStatus.Text = $"Status: {this.locInfo.GetBranchName(this.configSpecFilePath)}";
        }

        private async Task RefreshBranchesAsync()
        {
            this.lblLoading.Visible = true;
            Application.DoEvents();
            this.cmbBranch.Items.Clear();
            var branches = await Task.Run(() => GetBranchesAsync());
            if (branches != null)
            {
                this.cmbBranch.Items.AddRange(branches);
            }
            this.lblLoading.Visible = false;
        }

        private string[] GetBranchesAsync()
        {
            try
            {
                return locInfo.GetBranches();
            }
            catch (Exception ex)
            {
                mainLog.Error("refresh branch error!", ex);
                return null;
            }
        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {
            var oldcs = locInfo.GetConfigSpecOfView();
            if (radBranch.Checked)
            {
                var branch = this.cmbBranch.Text;
                var isBranchExist = IsBranchExist(branch);

                if (!isBranchExist)
                {
                    var ret = MessageBox.Show(
                        string.Format("[{0}] is not exist!\nDo you want to create it?", branch),
                        "Not Exist",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    if (ret == System.Windows.Forms.DialogResult.Yes)
                    {
                        CreateBranch(branch);
                    }
                }

                isBranchExist = IsBranchExist(branch);
                if (!isBranchExist) return;

                locInfo.UpdateConfigSpecBranch(branch, this.configSpecFilePath);
            }
            else if (radDeveloperMain.Checked)
            {
                locInfo.UpdateConfigSpecMain(this.configSpecFilePath);
            }
            else if (radVerifiableMain.Checked)
            {
                locInfo.ChangeToVerifyConfigSpec(this.configSpecFilePath);
            }
            else
            {
                return;
            }

            var newcs = locInfo.GetConfigSpecOfView();
            if (oldcs.Trim() == newcs.Trim())
            {
                MessageBox.Show("Already the newest!");
            }
            else
            {
                MessageBox.Show("Update successful!");
            }

            this.UpdateBranchStatus();
        }
    }
}
