using System.Windows.Forms;
using Telekit_Switch;

namespace TelekitOS_WindowsPreview.SystemWindows
{
    partial class FileExplorer
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
            this.tkToolbar1 = new TKToolbar(this);
            this.SuspendLayout();
            // 
            // tkToolbar1
            // 
            this.tkToolbar1.BackColor = System.Drawing.Color.SkyBlue;
            this.tkToolbar1.Location = new System.Drawing.Point(12, 12);
            this.tkToolbar1.Name = "tkToolbar1";
            this.tkToolbar1.Size = new System.Drawing.Size(587, 46);
            this.tkToolbar1.TabIndex = 0;
            // 
            // FileExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 437);
            this.ControlBox = false;
            this.Controls.Add(this.tkToolbar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FileExplorer";
            this.Load += new System.EventHandler(this.FileExplorer_Load);
            this.ResumeLayout(false);

        }
        #endregion

        private TKToolbar tkToolbar1;
    }
}