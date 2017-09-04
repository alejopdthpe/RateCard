namespace RateCard.OnPremises
{
    partial class AboutGUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            MetroFramework.Controls.MetroButton btnOk;
            MetroFramework.Controls.MetroButton btnContactSupport;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutGUI));
            this.label1 = new System.Windows.Forms.Label();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.lblVersion = new MetroFramework.Controls.MetroLabel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            btnOk = new MetroFramework.Controls.MetroButton();
            btnContactSupport = new MetroFramework.Controls.MetroButton();
            this.metroPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            btnOk.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(169)))), ((int)(((byte)(130)))));
            btnOk.CausesValidation = false;
            btnOk.ForeColor = System.Drawing.Color.White;
            btnOk.Location = new System.Drawing.Point(109, 438);
            btnOk.Name = "btnOk";
            btnOk.Size = new System.Drawing.Size(109, 44);
            btnOk.TabIndex = 14;
            btnOk.Text = "Ok";
            btnOk.UseCustomBackColor = true;
            btnOk.UseSelectable = true;
            btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnContactSupport
            // 
            btnContactSupport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(169)))), ((int)(((byte)(130)))));
            btnContactSupport.CausesValidation = false;
            btnContactSupport.ForeColor = System.Drawing.Color.White;
            btnContactSupport.Location = new System.Drawing.Point(318, 438);
            btnContactSupport.Name = "btnContactSupport";
            btnContactSupport.Size = new System.Drawing.Size(109, 44);
            btnContactSupport.TabIndex = 15;
            btnContactSupport.Text = "Contact Support";
            btnContactSupport.UseCustomBackColor = true;
            btnContactSupport.UseSelectable = true;
            btnContactSupport.Click += new System.EventHandler(this.btnContactSupport_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MetricHPE", 25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(342, 49);
            this.label1.TabIndex = 4;
            this.label1.Text = "DC - OSS Rate Card";
            // 
            // metroPanel1
            // 
            this.metroPanel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.metroPanel1.Controls.Add(this.metroLabel3);
            this.metroPanel1.Controls.Add(this.lblVersion);
            this.metroPanel1.Controls.Add(this.metroLabel2);
            this.metroPanel1.Controls.Add(this.metroLabel1);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 11;
            this.metroPanel1.Location = new System.Drawing.Point(32, 108);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(477, 217);
            this.metroPanel1.TabIndex = 5;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            this.metroPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.metroPanel1_Paint);
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.lblVersion.Location = new System.Drawing.Point(96, 81);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(88, 25);
            this.lblVersion.TabIndex = 4;
            this.lblVersion.Text = "lblVersion";
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel2.Location = new System.Drawing.Point(22, 81);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(78, 25);
            this.metroLabel2.TabIndex = 3;
            this.metroLabel2.Text = "Version: ";
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.Location = new System.Drawing.Point(22, 12);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(431, 25);
            this.metroLabel1.TabIndex = 2;
            this.metroLabel1.Text = "HPE Confidential © 2017 Hewlett Packard Enterprise ";
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel3.Location = new System.Drawing.Point(22, 143);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(329, 25);
            this.metroLabel3.TabIndex = 5;
            this.metroLabel3.Text = "Develop by: Pricing Development Team";
            // 
            // AboutGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(599, 607);
            this.Controls.Add(btnContactSupport);
            this.Controls.Add(btnOk);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("MetricHPE Medium", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AboutGUI";
            this.Padding = new System.Windows.Forms.Padding(20, 64, 20, 21);
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.Load += new System.EventHandler(this.AboutGUI_Load);
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroLabel lblVersion;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private MetroFramework.Controls.MetroLabel metroLabel3;
    }
}