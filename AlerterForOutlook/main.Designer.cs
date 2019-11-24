namespace OutlookReminder
{
    partial class main
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
        	this.components = new System.ComponentModel.Container();
        	System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main));
        	this.btnCheckNow = new System.Windows.Forms.Button();
        	this.timer1 = new System.Windows.Forms.Timer(this.components);
        	this.timerFlash = new System.Windows.Forms.Timer(this.components);
        	this.cBStartOnLogin = new System.Windows.Forms.CheckBox();
        	this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
        	this.tbSearchTitle = new System.Windows.Forms.TextBox();
        	this.label1 = new System.Windows.Forms.Label();
        	this.btnSaveSettings = new System.Windows.Forms.Button();
        	this.SuspendLayout();
        	// 
        	// btnCheckNow
        	// 
        	this.btnCheckNow.Location = new System.Drawing.Point(50, 33);
        	this.btnCheckNow.Name = "btnCheckNow";
        	this.btnCheckNow.Size = new System.Drawing.Size(615, 77);
        	this.btnCheckNow.TabIndex = 0;
        	this.btnCheckNow.Text = "Check Reminder Now";
        	this.btnCheckNow.UseVisualStyleBackColor = true;
        	this.btnCheckNow.Click += new System.EventHandler(this.btnCheckNow_Click);
        	// 
        	// timer1
        	// 
        	this.timer1.Enabled = true;
        	this.timer1.Interval = 15000;
        	this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
        	// 
        	// cBStartOnLogin
        	// 
        	this.cBStartOnLogin.AutoSize = true;
        	this.cBStartOnLogin.Location = new System.Drawing.Point(50, 315);
        	this.cBStartOnLogin.Name = "cBStartOnLogin";
        	this.cBStartOnLogin.Size = new System.Drawing.Size(347, 36);
        	this.cBStartOnLogin.TabIndex = 1;
        	this.cBStartOnLogin.Text = "start on Windows Login";
        	this.cBStartOnLogin.UseVisualStyleBackColor = true;
        	this.cBStartOnLogin.CheckedChanged += new System.EventHandler(this.cbStartOnLogin_CheckedChanged);
        	// 
        	// notifyIcon1
        	// 
        	this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
        	this.notifyIcon1.Text = "Alerter";
        	this.notifyIcon1.Visible = true;
        	this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
        	// 
        	// tbSearchTitle
        	// 
        	this.tbSearchTitle.Location = new System.Drawing.Point(50, 246);
        	this.tbSearchTitle.Name = "tbSearchTitle";
        	this.tbSearchTitle.Size = new System.Drawing.Size(397, 38);
        	this.tbSearchTitle.TabIndex = 3;
        	// 
        	// label1
        	// 
        	this.label1.Location = new System.Drawing.Point(50, 205);
        	this.label1.Name = "label1";
        	this.label1.Size = new System.Drawing.Size(397, 38);
        	this.label1.TabIndex = 4;
        	this.label1.Text = "Alerter Title contains";
        	// 
        	// btnSaveSettings
        	// 
        	this.btnSaveSettings.Location = new System.Drawing.Point(473, 246);
        	this.btnSaveSettings.Name = "btnSaveSettings";
        	this.btnSaveSettings.Size = new System.Drawing.Size(188, 77);
        	this.btnSaveSettings.TabIndex = 5;
        	this.btnSaveSettings.Text = "Save";
        	this.btnSaveSettings.UseVisualStyleBackColor = true;
        	this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
        	// 
        	// main
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.ClientSize = new System.Drawing.Size(713, 396);
        	this.Controls.Add(this.btnSaveSettings);
        	this.Controls.Add(this.label1);
        	this.Controls.Add(this.tbSearchTitle);
        	this.Controls.Add(this.cBStartOnLogin);
        	this.Controls.Add(this.btnCheckNow);
        	this.Name = "main";
        	this.Text = "Alerter";
        	this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.main_FormClosing);
        	this.Load += new System.EventHandler(this.main_Load);
        	this.ResumeLayout(false);
        	this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCheckNow;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timerFlash;
        private System.Windows.Forms.CheckBox cBStartOnLogin;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.TextBox tbSearchTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSaveSettings;
    }
}

