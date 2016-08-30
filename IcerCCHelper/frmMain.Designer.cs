namespace IcerDesign.CCHelper
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnRegisterExtension = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblVersion = new System.Windows.Forms.Label();
            this.webGuide = new System.Windows.Forms.WebBrowser();
            this.cmbServerList = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnRegisterExtension
            // 
            this.btnRegisterExtension.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRegisterExtension.Location = new System.Drawing.Point(261, 332);
            this.btnRegisterExtension.Name = "btnRegisterExtension";
            this.btnRegisterExtension.Size = new System.Drawing.Size(184, 23);
            this.btnRegisterExtension.TabIndex = 6;
            this.btnRegisterExtension.Text = "Install CC Extension";
            this.btnRegisterExtension.UseVisualStyleBackColor = true;
            this.btnRegisterExtension.Click += new System.EventHandler(this.btnRegisterExtension_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVersion.ForeColor = System.Drawing.Color.Blue;
            this.lblVersion.Location = new System.Drawing.Point(48, 324);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(170, 38);
            this.lblVersion.TabIndex = 8;
            this.lblVersion.Text = "Ver: 1.0.0.0\r\nEvaluation To: 2014-12-12";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // webGuide
            // 
            this.webGuide.AllowWebBrowserDrop = false;
            this.webGuide.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webGuide.Location = new System.Drawing.Point(0, 21);
            this.webGuide.MinimumSize = new System.Drawing.Size(20, 20);
            this.webGuide.Name = "webGuide";
            this.webGuide.Size = new System.Drawing.Size(457, 300);
            this.webGuide.TabIndex = 16;
            this.webGuide.WebBrowserShortcutsEnabled = false;
            // 
            // cmbServerList
            // 
            this.cmbServerList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbServerList.FormattingEnabled = true;
            this.cmbServerList.Location = new System.Drawing.Point(0, 0);
            this.cmbServerList.Name = "cmbServerList";
            this.cmbServerList.Size = new System.Drawing.Size(457, 21);
            this.cmbServerList.TabIndex = 17;
            this.cmbServerList.SelectedIndexChanged += new System.EventHandler(this.cmbServerList_SelectedIndexChanged);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 362);
            this.Controls.Add(this.cmbServerList);
            this.Controls.Add(this.webGuide);
            this.Controls.Add(this.btnRegisterExtension);
            this.Controls.Add(this.lblVersion);
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "frmMain";
            this.Text = "Icer Clear Case Helper";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Button btnRegisterExtension;
        private System.Windows.Forms.WebBrowser webGuide;
        private System.Windows.Forms.ComboBox cmbServerList;
    }
}

