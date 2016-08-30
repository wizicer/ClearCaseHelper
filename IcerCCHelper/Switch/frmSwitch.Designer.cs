namespace IcerDesign.CCHelper
{
    partial class frmSwitch
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
            this.btnInspect = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmbBranch = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblLoading = new System.Windows.Forms.Label();
            this.radVerifiableMain = new System.Windows.Forms.RadioButton();
            this.radDeveloperMain = new System.Windows.Forms.RadioButton();
            this.radBranch = new System.Windows.Forms.RadioButton();
            this.btnSwitch = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnInspect
            // 
            this.btnInspect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInspect.Location = new System.Drawing.Point(222, 12);
            this.btnInspect.Name = "btnInspect";
            this.btnInspect.Size = new System.Drawing.Size(87, 23);
            this.btnInspect.TabIndex = 3;
            this.btnInspect.Text = "Inspect";
            this.btnInspect.UseVisualStyleBackColor = true;
            this.btnInspect.Click += new System.EventHandler(this.btnInspect_Click);
            // 
            // cmbBranch
            // 
            this.cmbBranch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbBranch.FormattingEnabled = true;
            this.cmbBranch.Location = new System.Drawing.Point(108, 17);
            this.cmbBranch.Name = "cmbBranch";
            this.cmbBranch.Size = new System.Drawing.Size(176, 21);
            this.cmbBranch.TabIndex = 11;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblLoading);
            this.groupBox1.Controls.Add(this.radVerifiableMain);
            this.groupBox1.Controls.Add(this.radDeveloperMain);
            this.groupBox1.Controls.Add(this.radBranch);
            this.groupBox1.Controls.Add(this.cmbBranch);
            this.groupBox1.Location = new System.Drawing.Point(12, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(297, 91);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Switch To";
            // 
            // lblLoading
            // 
            this.lblLoading.AutoSize = true;
            this.lblLoading.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblLoading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.lblLoading.Location = new System.Drawing.Point(221, 20);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(45, 13);
            this.lblLoading.TabIndex = 13;
            this.lblLoading.Text = "Loading";
            this.lblLoading.Visible = false;
            // 
            // radVerifiableMain
            // 
            this.radVerifiableMain.AutoSize = true;
            this.radVerifiableMain.Location = new System.Drawing.Point(6, 64);
            this.radVerifiableMain.Name = "radVerifiableMain";
            this.radVerifiableMain.Size = new System.Drawing.Size(94, 17);
            this.radVerifiableMain.TabIndex = 12;
            this.radVerifiableMain.TabStop = true;
            this.radVerifiableMain.Text = "Verifiable Main";
            this.radVerifiableMain.UseVisualStyleBackColor = true;
            // 
            // radDeveloperMain
            // 
            this.radDeveloperMain.AutoSize = true;
            this.radDeveloperMain.Location = new System.Drawing.Point(6, 41);
            this.radDeveloperMain.Name = "radDeveloperMain";
            this.radDeveloperMain.Size = new System.Drawing.Size(100, 17);
            this.radDeveloperMain.TabIndex = 12;
            this.radDeveloperMain.TabStop = true;
            this.radDeveloperMain.Text = "Developer Main";
            this.radDeveloperMain.UseVisualStyleBackColor = true;
            // 
            // radBranch
            // 
            this.radBranch.AutoSize = true;
            this.radBranch.Location = new System.Drawing.Point(6, 18);
            this.radBranch.Name = "radBranch";
            this.radBranch.Size = new System.Drawing.Size(59, 17);
            this.radBranch.TabIndex = 12;
            this.radBranch.TabStop = true;
            this.radBranch.Text = "Branch";
            this.radBranch.UseVisualStyleBackColor = true;
            // 
            // btnSwitch
            // 
            this.btnSwitch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSwitch.Location = new System.Drawing.Point(222, 138);
            this.btnSwitch.Name = "btnSwitch";
            this.btnSwitch.Size = new System.Drawing.Size(87, 23);
            this.btnSwitch.TabIndex = 14;
            this.btnSwitch.Text = "Switch";
            this.btnSwitch.UseVisualStyleBackColor = true;
            this.btnSwitch.Click += new System.EventHandler(this.btnSwitch_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(9, 17);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(98, 13);
            this.lblStatus.TabIndex = 15;
            this.lblStatus.Text = "Current: Refreshing";
            // 
            // frmSwitch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 173);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnSwitch);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnInspect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmSwitch";
            this.Text = "Switch";
            this.Load += new System.EventHandler(this.frmSwitch_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInspect;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ComboBox cmbBranch;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radVerifiableMain;
        private System.Windows.Forms.RadioButton radDeveloperMain;
        private System.Windows.Forms.RadioButton radBranch;
        private System.Windows.Forms.Button btnSwitch;
        private System.Windows.Forms.Label lblLoading;
        private System.Windows.Forms.Label lblStatus;
    }
}

