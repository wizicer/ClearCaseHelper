namespace IcerDesign.CCHelper
{
    partial class frmSync
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSourceBasePath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSourceRule = new System.Windows.Forms.TextBox();
            this.txtSourcePassword = new System.Windows.Forms.TextBox();
            this.txtSourceUsername = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSourceBranch = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSourceUrl = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkDestSameAsSource = new System.Windows.Forms.CheckBox();
            this.txtDestPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDestRule = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lstChanges = new System.Windows.Forms.ListView();
            this.colhdrFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnSync = new System.Windows.Forms.Button();
            this.btnCompare = new System.Windows.Forms.Button();
            this.lblProgress = new System.Windows.Forms.Label();
            this.btnLoadSavedSessions = new System.Windows.Forms.Button();
            this.cmbSessions = new System.Windows.Forms.ComboBox();
            this.lblLoadedProfile = new System.Windows.Forms.Label();
            this.lnklblCheckError = new System.Windows.Forms.LinkLabel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSourceBasePath);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtSourceRule);
            this.groupBox1.Controls.Add(this.txtSourcePassword);
            this.groupBox1.Controls.Add(this.txtSourceUsername);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtSourceBranch);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtSourceUrl);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(466, 206);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Source Repository (git)";
            // 
            // txtSourceBasePath
            // 
            this.txtSourceBasePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSourceBasePath.Location = new System.Drawing.Point(260, 74);
            this.txtSourceBasePath.Name = "txtSourceBasePath";
            this.txtSourceBasePath.Size = new System.Drawing.Size(200, 20);
            this.txtSourceBasePath.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(201, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "BasePath";
            // 
            // txtSourceRule
            // 
            this.txtSourceRule.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSourceRule.Location = new System.Drawing.Point(9, 117);
            this.txtSourceRule.Multiline = true;
            this.txtSourceRule.Name = "txtSourceRule";
            this.txtSourceRule.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSourceRule.Size = new System.Drawing.Size(451, 84);
            this.txtSourceRule.TabIndex = 5;
            // 
            // txtSourcePassword
            // 
            this.txtSourcePassword.Location = new System.Drawing.Point(260, 45);
            this.txtSourcePassword.Name = "txtSourcePassword";
            this.txtSourcePassword.PasswordChar = '*';
            this.txtSourcePassword.Size = new System.Drawing.Size(135, 20);
            this.txtSourcePassword.TabIndex = 2;
            // 
            // txtSourceUsername
            // 
            this.txtSourceUsername.Location = new System.Drawing.Point(54, 45);
            this.txtSourceUsername.Name = "txtSourceUsername";
            this.txtSourceUsername.Size = new System.Drawing.Size(135, 20);
            this.txtSourceUsername.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(201, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Password";
            // 
            // txtSourceBranch
            // 
            this.txtSourceBranch.Location = new System.Drawing.Point(54, 74);
            this.txtSourceBranch.Name = "txtSourceBranch";
            this.txtSourceBranch.Size = new System.Drawing.Size(135, 20);
            this.txtSourceBranch.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(5, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Filter Rule:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Branch";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "URL";
            // 
            // txtSourceUrl
            // 
            this.txtSourceUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSourceUrl.Location = new System.Drawing.Point(54, 19);
            this.txtSourceUrl.Name = "txtSourceUrl";
            this.txtSourceUrl.Size = new System.Drawing.Size(406, 20);
            this.txtSourceUrl.TabIndex = 0;
            this.txtSourceUrl.TextChanged += new System.EventHandler(this.txtSourceUrl_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkDestSameAsSource);
            this.groupBox2.Controls.Add(this.txtDestPath);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtDestRule);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(259, 206);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Destination (ClearCase)";
            // 
            // chkDestSameAsSource
            // 
            this.chkDestSameAsSource.AutoSize = true;
            this.chkDestSameAsSource.Location = new System.Drawing.Point(69, 100);
            this.chkDestSameAsSource.Name = "chkDestSameAsSource";
            this.chkDestSameAsSource.Size = new System.Drawing.Size(105, 17);
            this.chkDestSameAsSource.TabIndex = 4;
            this.chkDestSameAsSource.Text = "Same As Source";
            this.chkDestSameAsSource.UseVisualStyleBackColor = true;
            this.chkDestSameAsSource.CheckedChanged += new System.EventHandler(this.chkDestSameAsSource_CheckedChanged);
            // 
            // txtDestPath
            // 
            this.txtDestPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDestPath.Location = new System.Drawing.Point(52, 19);
            this.txtDestPath.Name = "txtDestPath";
            this.txtDestPath.Size = new System.Drawing.Size(201, 20);
            this.txtDestPath.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Path";
            // 
            // txtDestRule
            // 
            this.txtDestRule.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDestRule.Location = new System.Drawing.Point(9, 117);
            this.txtDestRule.Multiline = true;
            this.txtDestRule.Name = "txtDestRule";
            this.txtDestRule.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDestRule.Size = new System.Drawing.Size(244, 83);
            this.txtDestRule.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 101);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Filter Rule:";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 43);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(729, 206);
            this.splitContainer1.SplitterDistance = 466;
            this.splitContainer1.TabIndex = 2;
            // 
            // lstChanges
            // 
            this.lstChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstChanges.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colhdrFilename,
            this.columnHeader1});
            this.lstChanges.GridLines = true;
            this.lstChanges.Location = new System.Drawing.Point(12, 255);
            this.lstChanges.Name = "lstChanges";
            this.lstChanges.Size = new System.Drawing.Size(729, 215);
            this.lstChanges.TabIndex = 5;
            this.lstChanges.UseCompatibleStateImageBehavior = false;
            this.lstChanges.View = System.Windows.Forms.View.Details;
            // 
            // colhdrFilename
            // 
            this.colhdrFilename.Text = "Filename";
            this.colhdrFilename.Width = 300;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Status";
            this.columnHeader1.Width = 90;
            // 
            // btnSync
            // 
            this.btnSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSync.Location = new System.Drawing.Point(666, 489);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(75, 23);
            this.btnSync.TabIndex = 6;
            this.btnSync.Text = "Synchronize";
            this.btnSync.UseVisualStyleBackColor = true;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // btnCompare
            // 
            this.btnCompare.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCompare.Location = new System.Drawing.Point(12, 489);
            this.btnCompare.Name = "btnCompare";
            this.btnCompare.Size = new System.Drawing.Size(75, 23);
            this.btnCompare.TabIndex = 7;
            this.btnCompare.Text = "Compare";
            this.btnCompare.UseVisualStyleBackColor = true;
            this.btnCompare.Click += new System.EventHandler(this.btnCompare_Click);
            // 
            // lblProgress
            // 
            this.lblProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblProgress.Location = new System.Drawing.Point(9, 473);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(732, 13);
            this.lblProgress.TabIndex = 8;
            // 
            // btnLoadSavedSessions
            // 
            this.btnLoadSavedSessions.Location = new System.Drawing.Point(12, 12);
            this.btnLoadSavedSessions.Name = "btnLoadSavedSessions";
            this.btnLoadSavedSessions.Size = new System.Drawing.Size(75, 23);
            this.btnLoadSavedSessions.TabIndex = 9;
            this.btnLoadSavedSessions.Text = "Load";
            this.btnLoadSavedSessions.UseVisualStyleBackColor = true;
            this.btnLoadSavedSessions.Click += new System.EventHandler(this.btnLoadSavedSessions_Click);
            // 
            // cmbSessions
            // 
            this.cmbSessions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSessions.FormattingEnabled = true;
            this.cmbSessions.Location = new System.Drawing.Point(93, 12);
            this.cmbSessions.Name = "cmbSessions";
            this.cmbSessions.Size = new System.Drawing.Size(163, 21);
            this.cmbSessions.TabIndex = 10;
            this.cmbSessions.SelectedIndexChanged += new System.EventHandler(this.cmbSessions_SelectedIndexChanged);
            // 
            // lblLoadedProfile
            // 
            this.lblLoadedProfile.AutoSize = true;
            this.lblLoadedProfile.Location = new System.Drawing.Point(269, 17);
            this.lblLoadedProfile.Name = "lblLoadedProfile";
            this.lblLoadedProfile.Size = new System.Drawing.Size(0, 13);
            this.lblLoadedProfile.TabIndex = 1;
            // 
            // lnklblCheckError
            // 
            this.lnklblCheckError.AutoSize = true;
            this.lnklblCheckError.Location = new System.Drawing.Point(275, 17);
            this.lnklblCheckError.Name = "lnklblCheckError";
            this.lnklblCheckError.Size = new System.Drawing.Size(29, 13);
            this.lnklblCheckError.TabIndex = 12;
            this.lnklblCheckError.TabStop = true;
            this.lnklblCheckError.Text = "Error";
            this.lnklblCheckError.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnklblCheckError_LinkClicked);
            // 
            // frmSync
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 524);
            this.Controls.Add(this.lnklblCheckError);
            this.Controls.Add(this.cmbSessions);
            this.Controls.Add(this.btnLoadSavedSessions);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.btnCompare);
            this.Controls.Add(this.btnSync);
            this.Controls.Add(this.lblLoadedProfile);
            this.Controls.Add(this.lstChanges);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "frmSync";
            this.Text = "Semi-auto synchronizer";
            this.Load += new System.EventHandler(this.frmSync_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtSourceBranch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSourceUrl;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtDestPath;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView lstChanges;
        private System.Windows.Forms.ColumnHeader colhdrFilename;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.Button btnCompare;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.TextBox txtSourceBasePath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSourceRule;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDestRule;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkDestSameAsSource;
        private System.Windows.Forms.Button btnLoadSavedSessions;
        private System.Windows.Forms.ComboBox cmbSessions;
        private System.Windows.Forms.Label lblLoadedProfile;
        private System.Windows.Forms.LinkLabel lnklblCheckError;
        private System.Windows.Forms.TextBox txtSourcePassword;
        private System.Windows.Forms.TextBox txtSourceUsername;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
    }
}