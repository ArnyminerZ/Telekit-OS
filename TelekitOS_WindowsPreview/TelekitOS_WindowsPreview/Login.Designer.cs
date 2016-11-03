namespace TelekitOS_WindowsPreview
{
    partial class Login
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
            this.materialTabSelector1 = new MaterialSkin.Controls.MaterialTabSelector();
            this.userBox = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.passBox = new MaterialSkin.Controls.MaterialSingleLineTextField();
            this.materialRaisedButton1 = new MaterialSkin.Controls.MaterialRaisedButton();
            this.SuspendLayout();
            // 
            // materialTabSelector1
            // 
            this.materialTabSelector1.BaseTabControl = null;
            this.materialTabSelector1.Depth = 0;
            this.materialTabSelector1.Location = new System.Drawing.Point(-10, 27);
            this.materialTabSelector1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabSelector1.Name = "materialTabSelector1";
            this.materialTabSelector1.Size = new System.Drawing.Size(871, 395);
            this.materialTabSelector1.TabIndex = 0;
            this.materialTabSelector1.Text = "materialTabSelector1";
            // 
            // userBox
            // 
            this.userBox.Depth = 0;
            this.userBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.userBox.Hint = "Nom d\'usuari";
            this.userBox.Location = new System.Drawing.Point(222, 109);
            this.userBox.MaxLength = 32767;
            this.userBox.MouseState = MaterialSkin.MouseState.HOVER;
            this.userBox.Name = "userBox";
            this.userBox.PasswordChar = '\0';
            this.userBox.SelectedText = "";
            this.userBox.SelectionLength = 0;
            this.userBox.SelectionStart = 0;
            this.userBox.Size = new System.Drawing.Size(222, 23);
            this.userBox.TabIndex = 1;
            this.userBox.TabStop = false;
            this.userBox.UseSystemPasswordChar = false;
            // 
            // passBox
            // 
            this.passBox.Depth = 0;
            this.passBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passBox.Hint = "Contrasenya";
            this.passBox.Location = new System.Drawing.Point(222, 138);
            this.passBox.MaxLength = 32767;
            this.passBox.MouseState = MaterialSkin.MouseState.HOVER;
            this.passBox.Name = "passBox";
            this.passBox.PasswordChar = '•';
            this.passBox.SelectedText = "";
            this.passBox.SelectionLength = 0;
            this.passBox.SelectionStart = 0;
            this.passBox.Size = new System.Drawing.Size(222, 23);
            this.passBox.TabIndex = 2;
            this.passBox.TabStop = false;
            this.passBox.UseSystemPasswordChar = false;
            // 
            // materialRaisedButton1
            // 
            this.materialRaisedButton1.AutoSize = true;
            this.materialRaisedButton1.Depth = 0;
            this.materialRaisedButton1.Icon = null;
            this.materialRaisedButton1.Location = new System.Drawing.Point(222, 168);
            this.materialRaisedButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialRaisedButton1.Name = "materialRaisedButton1";
            this.materialRaisedButton1.Primary = true;
            this.materialRaisedButton1.Size = new System.Drawing.Size(222, 36);
            this.materialRaisedButton1.TabIndex = 3;
            this.materialRaisedButton1.Text = "Entrar";
            this.materialRaisedButton1.UseVisualStyleBackColor = true;
            this.materialRaisedButton1.Click += new System.EventHandler(this.materialRaisedButton1_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 399);
            this.Controls.Add(this.materialRaisedButton1);
            this.Controls.Add(this.passBox);
            this.Controls.Add(this.userBox);
            this.Controls.Add(this.materialTabSelector1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Login";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialTabSelector materialTabSelector1;
        private MaterialSkin.Controls.MaterialSingleLineTextField userBox;
        private MaterialSkin.Controls.MaterialSingleLineTextField passBox;
        private MaterialSkin.Controls.MaterialRaisedButton materialRaisedButton1;
    }
}