namespace RateCard.OnPremises
{
    partial class frmCredentials
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
            MetroFramework.Controls.MetroButton metroButton1;
            MetroFramework.Controls.MetroButton btnCancel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCredentials));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblMainTitle = new System.Windows.Forms.Label();
            this.lblAdministration = new System.Windows.Forms.Label();
            this.txtEmail = new MetroFramework.Controls.MetroTextBox();
            this.txtPassword = new MetroFramework.Controls.MetroTextBox();
            metroButton1 = new MetroFramework.Controls.MetroButton();
            btnCancel = new MetroFramework.Controls.MetroButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // metroButton1
            // 
            metroButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(169)))), ((int)(((byte)(130)))));
            metroButton1.CausesValidation = false;
            metroButton1.ForeColor = System.Drawing.Color.White;
            metroButton1.Location = new System.Drawing.Point(236, 443);
            metroButton1.Name = "metroButton1";
            metroButton1.Size = new System.Drawing.Size(109, 44);
            metroButton1.TabIndex = 14;
            metroButton1.Text = "Acess";
            metroButton1.UseCustomBackColor = true;
            metroButton1.UseCustomForeColor = true;
            metroButton1.UseSelectable = true;
            metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // btnCancel
            // 
            btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(169)))), ((int)(((byte)(130)))));
            btnCancel.CausesValidation = false;
            btnCancel.ForeColor = System.Drawing.Color.White;
            btnCancel.Location = new System.Drawing.Point(49, 549);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(109, 44);
            btnCancel.TabIndex = 17;
            btnCancel.Text = "Cancel";
            btnCancel.UseCustomBackColor = true;
            btnCancel.UseCustomForeColor = true;
            btnCancel.UseSelectable = true;
            btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::RateCard.OnPremises.Properties.Resources.hpesm_pri_grn_pos_rgb;
            this.pictureBox1.Location = new System.Drawing.Point(11, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(179, 71);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // lblMainTitle
            // 
            this.lblMainTitle.AutoSize = true;
            this.lblMainTitle.Font = new System.Drawing.Font("MetricHPE", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMainTitle.Location = new System.Drawing.Point(114, 116);
            this.lblMainTitle.Name = "lblMainTitle";
            this.lblMainTitle.Size = new System.Drawing.Size(342, 49);
            this.lblMainTitle.TabIndex = 4;
            this.lblMainTitle.Text = "DC - OSS Rate Card";
            // 
            // lblAdministration
            // 
            this.lblAdministration.AutoSize = true;
            this.lblAdministration.BackColor = System.Drawing.Color.Transparent;
            this.lblAdministration.Font = new System.Drawing.Font("MetricHPE Semibold", 21F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdministration.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblAdministration.Location = new System.Drawing.Point(42, 175);
            this.lblAdministration.Name = "lblAdministration";
            this.lblAdministration.Size = new System.Drawing.Size(491, 40);
            this.lblAdministration.TabIndex = 9;
            this.lblAdministration.Text = "Administration | Tool Maintenance";
            // 
            // txtEmail
            // 
            // 
            // 
            // 
            this.txtEmail.CustomButton.FlatAppearance.BorderColor = System.Drawing.Color.Yellow;
            this.txtEmail.CustomButton.FlatAppearance.BorderSize = 4;
            this.txtEmail.CustomButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.txtEmail.CustomButton.Font = new System.Drawing.Font("MetricHPE", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.CustomButton.Image = null;
            this.txtEmail.CustomButton.Location = new System.Drawing.Point(237, 2);
            this.txtEmail.CustomButton.Name = "";
            this.txtEmail.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.txtEmail.CustomButton.Style = MetroFramework.MetroColorStyle.Green;
            this.txtEmail.CustomButton.TabIndex = 1;
            this.txtEmail.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtEmail.CustomButton.UseSelectable = true;
            this.txtEmail.CustomButton.Visible = false;
            this.txtEmail.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtEmail.Lines = new string[0];
            this.txtEmail.Location = new System.Drawing.Point(162, 274);
            this.txtEmail.MaxLength = 32767;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PasswordChar = '\0';
            this.txtEmail.PromptText = "Enter your email here";
            this.txtEmail.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtEmail.SelectedText = "";
            this.txtEmail.SelectionLength = 0;
            this.txtEmail.SelectionStart = 0;
            this.txtEmail.ShortcutsEnabled = true;
            this.txtEmail.Size = new System.Drawing.Size(265, 30);
            this.txtEmail.Style = MetroFramework.MetroColorStyle.Green;
            this.txtEmail.TabIndex = 15;
            this.txtEmail.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtEmail.UseSelectable = true;
            this.txtEmail.UseStyleColors = true;
            this.txtEmail.WaterMark = "Enter your email here";
            this.txtEmail.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtEmail.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // txtPassword
            // 
            // 
            // 
            // 
            this.txtPassword.CustomButton.FlatAppearance.BorderColor = System.Drawing.Color.Yellow;
            this.txtPassword.CustomButton.FlatAppearance.BorderSize = 4;
            this.txtPassword.CustomButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.txtPassword.CustomButton.Font = new System.Drawing.Font("MetricHPE", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.CustomButton.Image = null;
            this.txtPassword.CustomButton.Location = new System.Drawing.Point(237, 2);
            this.txtPassword.CustomButton.Name = "";
            this.txtPassword.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.txtPassword.CustomButton.Style = MetroFramework.MetroColorStyle.Green;
            this.txtPassword.CustomButton.TabIndex = 1;
            this.txtPassword.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtPassword.CustomButton.UseSelectable = true;
            this.txtPassword.CustomButton.Visible = false;
            this.txtPassword.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.txtPassword.Lines = new string[0];
            this.txtPassword.Location = new System.Drawing.Point(162, 355);
            this.txtPassword.MaxLength = 32767;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.PromptText = "Enter your password here";
            this.txtPassword.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtPassword.SelectedText = "";
            this.txtPassword.SelectionLength = 0;
            this.txtPassword.SelectionStart = 0;
            this.txtPassword.ShortcutsEnabled = true;
            this.txtPassword.Size = new System.Drawing.Size(265, 30);
            this.txtPassword.Style = MetroFramework.MetroColorStyle.Green;
            this.txtPassword.TabIndex = 16;
            this.txtPassword.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtPassword.UseSelectable = true;
            this.txtPassword.UseStyleColors = true;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.WaterMark = "Enter your password here";
            this.txtPassword.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtPassword.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.txtPassword.WithError = true;
            // 
            // frmCredentials
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 616);
            this.Controls.Add(btnCancel);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(metroButton1);
            this.Controls.Add(this.lblAdministration);
            this.Controls.Add(this.lblMainTitle);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCredentials";
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCredentials_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmCredentials_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblMainTitle;
        private System.Windows.Forms.Label lblAdministration;
        private MetroFramework.Controls.MetroTextBox txtEmail;
        private MetroFramework.Controls.MetroTextBox txtPassword;
    }
}