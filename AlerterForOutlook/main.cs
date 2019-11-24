using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Microsoft.Win32;

namespace OutlookReminder
{
    public partial class main : Form
    {

        /// <summary>
        /// Commandline Arguments
        /// </summary>

        public string[] args;
        string searchTitle = "";
        alerter alert;

        #region main

        public main()
        {
            InitializeComponent();
            args = Environment.GetCommandLineArgs();
            alert = new alerter();
        }
        
        private void main_Load(object sender, EventArgs e)
        {
            
            searchTitle = OutlookReminder.Properties.Settings1.Default.ReminderTitle;
            tbSearchTitle.Text = searchTitle;
            notifyIcon1.Visible = true;            
          
            if (checkRegistry()== true)
            {
                cBStartOnLogin.Checked = true;
            }
            checkReminder();

        }
        
        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((e.CloseReason != CloseReason.UserClosing))
            {

            }
            else
            {
                timer1.Interval = 30000;
                this.WindowState = FormWindowState.Minimized;  // TODO
                this.Hide();
                e.Cancel = true;

            }
        }
        
        #endregion

        #region Registry
        
        
        /// <summary>
        /// set "run" key to this application
        /// </summary>
        /// 
        private void setRegistry()
        {
            RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");

            if (key != null)
            {
                string z = key.GetValue("DerOutlookAlerter", "").ToString();
                RegistryKey k = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");
                k.SetValue("DerOutlookAlerter", args[0], RegistryValueKind.String);
            }

        }

        private Boolean checkRegistry()
        {
            string z = "";
            Boolean rc = false;

            RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");

            if (key != null)
            {
                z = key.GetValue("DerOutlookAlerter", "").ToString();
            }
            if (z.Length != 0) rc = true;
            return (rc);
        }

        private void clearRegistry()
        {
            string z = "";

            RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");

            if (key != null)
            {
                RegistryKey k = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");
                k.DeleteValue("DerOutlookAlerter");
            }

        }

        #endregion
        
        /// <summary>
        /// show alerter window if outlook reminder dialog exists
        /// </summary>

        private void checkReminder()
        {
            Process[] processes = Process.GetProcesses();
            Boolean found = false;
            
            string name = "";
            string title = "";

            foreach (Process p in processes)
            {
                name = p.ProcessName;
                title = p.MainWindowTitle;

                Application.DoEvents();

                if (name == "OUTLOOK" & title.Contains(searchTitle))
                {
                    found = true;
                    timer1.Interval = 10000;
                    
                    if (alert.isShown == false) 
                    {
                        alert.Show();
                        alert.BringToFront();
                        alert.WindowState = FormWindowState.Normal;
                        //alert.WindowState = FormWindowState.Maximized;
                    }
                }

            }

            if (found == false)
            {
                alert.doClose();
                timer1.Interval = 30000;
            }

        }

        private void btnCheckNow_Click(object sender, EventArgs e)
        {
            checkReminder();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            checkReminder();
        }


        private void cbStartOnLogin_CheckedChanged(object sender, EventArgs e)
        {
            if (cBStartOnLogin.Checked == true) {
                setRegistry();
            }
            else {
                clearRegistry();
            }
        }


        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.BringToFront();
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.BringToFront();
        }

        
        void btnSaveSettings_Click(object sender, EventArgs e)
        {
            OutlookReminder.Properties.Settings1.Default.ReminderTitle = tbSearchTitle.Text;
            OutlookReminder.Properties.Settings1.Default.Save();
            searchTitle = OutlookReminder.Properties.Settings1.Default.ReminderTitle;
        }
    }
}
