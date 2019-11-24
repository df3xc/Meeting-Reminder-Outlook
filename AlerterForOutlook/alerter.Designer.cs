namespace OutlookReminder
{
    partial class alerter
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
        	this.timer1 = new System.Windows.Forms.Timer(this.components);
        	this.label1 = new System.Windows.Forms.Label();
        	this.SuspendLayout();
        	// 
        	// timer1
        	// 
        	this.timer1.Enabled = true;
        	this.timer1.Interval = 500;
        	this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
        	// 
        	// label1
        	// 
        	this.label1.AutoSize = true;
        	this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.label1.Location = new System.Drawing.Point(600, 42);
        	this.label1.Name = "label1";
        	this.label1.Size = new System.Drawing.Size(1058, 69);
        	this.label1.TabIndex = 0;
        	this.label1.Text = "Check your Outlook Meeting Reminder";
        	// 
        	// alerter
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.BackColor = System.Drawing.Color.Salmon;
        	this.ClientSize = new System.Drawing.Size(2099, 159);
        	this.Controls.Add(this.label1);
        	this.Name = "alerter";
        	this.Text = "Alerter";
        	this.TopMost = true;
        	this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.alerter_FormClosing);
        	this.Load += new System.EventHandler(this.alerter_Load);
        	this.Shown += new System.EventHandler(this.alerter_Shown);
        	this.ResumeLayout(false);
        	this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
    }
}