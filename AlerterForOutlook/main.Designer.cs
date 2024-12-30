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
            this.timerCheckOutlook = new System.Windows.Forms.Timer(this.components);
            this.cBStartOnLogin = new System.Windows.Forms.CheckBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.tbSearchTitle = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.lbPegel = new System.Windows.Forms.Label();
            this.timerPegel = new System.Windows.Forms.Timer(this.components);
            this.cbPause = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbMailCounter = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCheckNow
            // 
            this.btnCheckNow.Location = new System.Drawing.Point(38, 27);
            this.btnCheckNow.Margin = new System.Windows.Forms.Padding(2);
            this.btnCheckNow.Name = "btnCheckNow";
            this.btnCheckNow.Size = new System.Drawing.Size(461, 62);
            this.btnCheckNow.TabIndex = 0;
            this.btnCheckNow.Text = "Check Reminder Now";
            this.btnCheckNow.UseVisualStyleBackColor = true;
            this.btnCheckNow.Click += new System.EventHandler(this.btnCheckNow_Click);
            // 
            // timerCheckOutlook
            // 
            this.timerCheckOutlook.Enabled = true;
            this.timerCheckOutlook.Interval = 15000;
            this.timerCheckOutlook.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // cBStartOnLogin
            // 
            this.cBStartOnLogin.AutoSize = true;
            this.cBStartOnLogin.Location = new System.Drawing.Point(38, 210);
            this.cBStartOnLogin.Margin = new System.Windows.Forms.Padding(2);
            this.cBStartOnLogin.Name = "cBStartOnLogin";
            this.cBStartOnLogin.Size = new System.Drawing.Size(268, 29);
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
            this.tbSearchTitle.Location = new System.Drawing.Point(38, 154);
            this.tbSearchTitle.Margin = new System.Windows.Forms.Padding(2);
            this.tbSearchTitle.Name = "tbSearchTitle";
            this.tbSearchTitle.Size = new System.Drawing.Size(299, 31);
            this.tbSearchTitle.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(39, 121);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(298, 31);
            this.label1.TabIndex = 4;
            this.label1.Text = "Alerter Title contains";
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.Location = new System.Drawing.Point(358, 121);
            this.btnSaveSettings.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(141, 62);
            this.btnSaveSettings.TabIndex = 5;
            this.btnSaveSettings.Text = "Save";
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
            // 
            // lbPegel
            // 
            this.lbPegel.AutoSize = true;
            this.lbPegel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPegel.Location = new System.Drawing.Point(365, 323);
            this.lbPegel.Name = "lbPegel";
            this.lbPegel.Size = new System.Drawing.Size(94, 37);
            this.lbPegel.TabIndex = 6;
            this.lbPegel.Text = "pegel";
            // 
            // timerPegel
            // 
            this.timerPegel.Interval = 30000;
            this.timerPegel.Tick += new System.EventHandler(this.timerPegel_Tick);
            // 
            // cbPause
            // 
            this.cbPause.AutoSize = true;
            this.cbPause.Location = new System.Drawing.Point(38, 256);
            this.cbPause.Name = "cbPause";
            this.cbPause.Size = new System.Drawing.Size(105, 29);
            this.cbPause.TabIndex = 7;
            this.cbPause.Text = "Pause";
            this.cbPause.UseVisualStyleBackColor = true;
            this.cbPause.CheckedChanged += new System.EventHandler(this.cbPause_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(39, 329);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(298, 31);
            this.label2.TabIndex = 8;
            this.label2.Text = "Neckarpegel Gundelsheim";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(39, 387);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(460, 35);
            this.label3.TabIndex = 9;
            this.label3.Text = "Registry Key \"DerOutlookAlerter\"";
            // 
            // lbMailCounter
            // 
            this.lbMailCounter.AutoSize = true;
            this.lbMailCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMailCounter.Location = new System.Drawing.Point(365, 235);
            this.lbMailCounter.Name = "lbMailCounter";
            this.lbMailCounter.Size = new System.Drawing.Size(192, 37);
            this.lbMailCounter.TabIndex = 10;
            this.lbMailCounter.Text = "mail counter";
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Chocolate;
            this.ClientSize = new System.Drawing.Size(583, 453);
            this.Controls.Add(this.lbMailCounter);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbPause);
            this.Controls.Add(this.lbPegel);
            this.Controls.Add(this.btnSaveSettings);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbSearchTitle);
            this.Controls.Add(this.cBStartOnLogin);
            this.Controls.Add(this.btnCheckNow);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "main";
            this.Text = "Alerter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.main_FormClosing);
            this.Load += new System.EventHandler(this.main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCheckNow;
        private System.Windows.Forms.Timer timerCheckOutlook;
        private System.Windows.Forms.CheckBox cBStartOnLogin;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.TextBox tbSearchTitle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSaveSettings;
        private System.Windows.Forms.Label lbPegel;
        private System.Windows.Forms.Timer timerPegel;
        private System.Windows.Forms.CheckBox cbPause;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbMailCounter;
    }
}

