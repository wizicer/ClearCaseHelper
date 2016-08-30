namespace IcerDesign.CCHelper
{
    partial class frmCommitter
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
            this.chkDoNotModifyConfigSpecTxt = new System.Windows.Forms.CheckBox();
            this.txtAlternativeLabel = new System.Windows.Forms.TextBox();
            this.chkUseAlternativeLabel = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnRefreshCommit = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btnCommit = new System.Windows.Forms.Button();
            this.lstChanges = new System.Windows.Forms.ListView();
            this.colhdrFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnubtnRevert = new System.Windows.Forms.ToolStripMenuItem();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCommitComment = new System.Windows.Forms.TextBox();
            this.lblLabelToCommit = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkIgnoreModify = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkDoNotModifyConfigSpecTxt
            // 
            this.chkDoNotModifyConfigSpecTxt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkDoNotModifyConfigSpecTxt.AutoSize = true;
            this.chkDoNotModifyConfigSpecTxt.Location = new System.Drawing.Point(6, 453);
            this.chkDoNotModifyConfigSpecTxt.Name = "chkDoNotModifyConfigSpecTxt";
            this.chkDoNotModifyConfigSpecTxt.Size = new System.Drawing.Size(168, 17);
            this.chkDoNotModifyConfigSpecTxt.TabIndex = 11;
            this.chkDoNotModifyConfigSpecTxt.Text = "Do NOT modify configspec.txt";
            this.chkDoNotModifyConfigSpecTxt.UseVisualStyleBackColor = true;
            // 
            // txtAlternativeLabel
            // 
            this.txtAlternativeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAlternativeLabel.Enabled = false;
            this.txtAlternativeLabel.Location = new System.Drawing.Point(427, 449);
            this.txtAlternativeLabel.Name = "txtAlternativeLabel";
            this.txtAlternativeLabel.Size = new System.Drawing.Size(100, 20);
            this.txtAlternativeLabel.TabIndex = 10;
            // 
            // chkUseAlternativeLabel
            // 
            this.chkUseAlternativeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkUseAlternativeLabel.AutoSize = true;
            this.chkUseAlternativeLabel.Location = new System.Drawing.Point(288, 451);
            this.chkUseAlternativeLabel.Name = "chkUseAlternativeLabel";
            this.chkUseAlternativeLabel.Size = new System.Drawing.Size(133, 17);
            this.chkUseAlternativeLabel.TabIndex = 9;
            this.chkUseAlternativeLabel.Text = "Use Alternative Label: ";
            this.chkUseAlternativeLabel.UseVisualStyleBackColor = true;
            this.chkUseAlternativeLabel.CheckedChanged += new System.EventHandler(this.chkUseAlternativeLabel_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 92);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(166, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "e.g: Icer: remove PI reset variable";
            // 
            // btnRefreshCommit
            // 
            this.btnRefreshCommit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshCommit.Location = new System.Drawing.Point(476, 17);
            this.btnRefreshCommit.Name = "btnRefreshCommit";
            this.btnRefreshCommit.Size = new System.Drawing.Size(135, 23);
            this.btnRefreshCommit.TabIndex = 7;
            this.btnRefreshCommit.Text = "Refresh";
            this.btnRefreshCommit.UseVisualStyleBackColor = true;
            this.btnRefreshCommit.Click += new System.EventHandler(this.btnRefreshCommit_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 111);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Changes:";
            // 
            // btnCommit
            // 
            this.btnCommit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCommit.Enabled = false;
            this.btnCommit.Location = new System.Drawing.Point(533, 447);
            this.btnCommit.Name = "btnCommit";
            this.btnCommit.Size = new System.Drawing.Size(75, 23);
            this.btnCommit.TabIndex = 5;
            this.btnCommit.Text = "Commit";
            this.btnCommit.UseVisualStyleBackColor = true;
            this.btnCommit.Click += new System.EventHandler(this.btnCommit_Click);
            // 
            // lstChanges
            // 
            this.lstChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstChanges.CheckBoxes = true;
            this.lstChanges.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colhdrFilename,
            this.columnHeader1});
            this.lstChanges.ContextMenuStrip = this.contextMenuStrip1;
            this.lstChanges.Enabled = false;
            this.lstChanges.GridLines = true;
            this.lstChanges.Location = new System.Drawing.Point(9, 127);
            this.lstChanges.Name = "lstChanges";
            this.lstChanges.Size = new System.Drawing.Size(599, 314);
            this.lstChanges.TabIndex = 4;
            this.lstChanges.UseCompatibleStateImageBehavior = false;
            this.lstChanges.View = System.Windows.Forms.View.Details;
            this.lstChanges.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstChanges_KeyDown);
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
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnubtnRevert});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(108, 26);
            // 
            // mnubtnRevert
            // 
            this.mnubtnRevert.Name = "mnubtnRevert";
            this.mnubtnRevert.Size = new System.Drawing.Size(107, 22);
            this.mnubtnRevert.Text = "Revert";
            this.mnubtnRevert.Click += new System.EventHandler(this.mnubtnRevert_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(91, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Commit Comment:";
            // 
            // txtCommitComment
            // 
            this.txtCommitComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCommitComment.Enabled = false;
            this.txtCommitComment.Location = new System.Drawing.Point(9, 69);
            this.txtCommitComment.Name = "txtCommitComment";
            this.txtCommitComment.Size = new System.Drawing.Size(599, 20);
            this.txtCommitComment.TabIndex = 2;
            // 
            // lblLabelToCommit
            // 
            this.lblLabelToCommit.AutoSize = true;
            this.lblLabelToCommit.ForeColor = System.Drawing.Color.Blue;
            this.lblLabelToCommit.Location = new System.Drawing.Point(70, 16);
            this.lblLabelToCommit.Name = "lblLabelToCommit";
            this.lblLabelToCommit.Size = new System.Drawing.Size(110, 13);
            this.lblLabelToCommit.TabIndex = 1;
            this.lblLabelToCommit.Text = "Please Refresh First!!!";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Commit To:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.chkIgnoreModify);
            this.groupBox1.Controls.Add(this.chkDoNotModifyConfigSpecTxt);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtAlternativeLabel);
            this.groupBox1.Controls.Add(this.lblLabelToCommit);
            this.groupBox1.Controls.Add(this.chkUseAlternativeLabel);
            this.groupBox1.Controls.Add(this.txtCommitComment);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.btnRefreshCommit);
            this.groupBox1.Controls.Add(this.lstChanges);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.btnCommit);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(614, 476);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Commit";
            // 
            // chkIgnoreModify
            // 
            this.chkIgnoreModify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkIgnoreModify.AutoSize = true;
            this.chkIgnoreModify.Checked = true;
            this.chkIgnoreModify.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIgnoreModify.Enabled = false;
            this.chkIgnoreModify.Location = new System.Drawing.Point(346, 21);
            this.chkIgnoreModify.Name = "chkIgnoreModify";
            this.chkIgnoreModify.Size = new System.Drawing.Size(124, 17);
            this.chkIgnoreModify.TabIndex = 12;
            this.chkIgnoreModify.Text = "Ignore Modify Check";
            this.chkIgnoreModify.UseVisualStyleBackColor = true;
            // 
            // frmCommitter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 500);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmCommitter";
            this.Text = "Commit";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnRefreshCommit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnCommit;
        private System.Windows.Forms.ListView lstChanges;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCommitComment;
        private System.Windows.Forms.Label lblLabelToCommit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ColumnHeader colhdrFilename;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnubtnRevert;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtAlternativeLabel;
        private System.Windows.Forms.CheckBox chkUseAlternativeLabel;
        private System.Windows.Forms.CheckBox chkDoNotModifyConfigSpecTxt;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox chkIgnoreModify;
    }
}

