namespace IcerDesign.CCHelper
{
    partial class frmRunCommand
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
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.prgbarCommands = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // txtStatus
            // 
            this.txtStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStatus.Location = new System.Drawing.Point(0, 1);
            this.txtStatus.Multiline = true;
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.Size = new System.Drawing.Size(526, 242);
            this.txtStatus.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.Enabled = false;
            this.btnClose.Location = new System.Drawing.Point(451, 249);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // prgbarCommands
            // 
            this.prgbarCommands.Location = new System.Drawing.Point(0, 249);
            this.prgbarCommands.Name = "prgbarCommands";
            this.prgbarCommands.Size = new System.Drawing.Size(434, 23);
            this.prgbarCommands.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.prgbarCommands.TabIndex = 2;
            // 
            // frmRunCommand
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 273);
            this.Controls.Add(this.prgbarCommands);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtStatus);
            this.Name = "frmRunCommand";
            this.Text = "Commands Running Window";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ProgressBar prgbarCommands;
    }
}