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

    public partial class frmMerge : Form
    {
        private static log4net.ILog mainLog = log4net.LogManager.GetLogger("main");

        private LocationInfo locInfo;

        public frmMerge(LocationInfo locationInfo)
        {
            this.locInfo = locationInfo;
            this.InitializeComponent();
        }

        private async void frmMerge_Load(object sender, EventArgs e)
        {
            await this.RefreshBranchesAsync();
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

        private void btnMerge_Click(object sender, EventArgs e)
        {
            var branch = this.radBranch.Checked ? this.cmbBranch.Text : null;
            if (string.IsNullOrEmpty(branch))
            {
                MessageBox.Show("branch shouldn't be empty!");
                return;
            }

            locInfo.Merge(branch);
        }
    }
}
