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
using System.Threading;
using System.IO;
using System.Runtime.InteropServices;
using WebTest;

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
        Alarm NeckarAlarm;
        neckar pegel;
        sendmail mail;

        public int pause_counter = 0;
        public int send_mail_counter = 0;
        public float neckar_pegel_warning = 0;
        public float neckar_alarm_pegel = 0;
        public int time_intervall = 30;
        public int time_default_intervall = 120;
        public int time = 0;
        public int old_time = 0;
        public Boolean mail_done = false;

        #region main

        public main()
        {
            InitializeComponent();
            args = Environment.GetCommandLineArgs();
            alert = new alerter();
            pegel = new neckar();
            NeckarAlarm = new Alarm();
            mail = new sendmail();
            timerPegel.Enabled = true;
            timerCheckOutlook.Enabled = true;

            time_intervall = time_default_intervall;
        }
        
        private void main_Load(object sender, EventArgs e)
        {
            searchTitle = OutlookReminder.Properties.Settings1.Default.ReminderTitle;
            neckar_pegel_warning = OutlookReminder.Properties.Settings1.Default.WarningLevel;
            neckar_alarm_pegel = OutlookReminder.Properties.Settings1.Default.Alarmlevel;
            tbSearchTitle.Text = searchTitle;
            notifyIcon1.Visible = true;            
          
            if (checkRegistry()== true)
            {
                cBStartOnLogin.Checked = true;
            }
            checkReminderWindow();
            verifyWaterLevel();
            float neckarpegel = pegel.getWaterLevel("Gundelsheim UP");
            lbPegel.Text = neckarpegel.ToString();
        }
        
        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((e.CloseReason != CloseReason.UserClosing))
            {

            }
            else
            {
                timerCheckOutlook.Interval = 30000;
                this.WindowState = FormWindowState.Minimized;  // TODO
                this.Hide();
                e.Cancel = true;

            }
        }
        
        #endregion

        #region Registry
        
        
        /// <summary>
        /// set "run" key to this application in the registry
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

        /// <summary>
        /// remove "run" key in the registry
        /// </summary>
        
        private void clearRegistry()
        {
            RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");

            if (key != null)
            {
                RegistryKey k = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");
                k.DeleteValue("DerOutlookAlerter");
            }

        }

        #endregion
        
        /// <summary>
        /// show alerter window if outlook reminder dialog process exists
        /// This does not work if you select the outlook main window.
        /// The Reminder Dialog is then not in the list of processes
        /// </summary>

        private void checkReminderProcess()
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
                //ogBox.AppendText(name + " : " + title +"\n");                
                if (name == "OUTLOOK")
                {
                	//logBox.AppendText(name + " : " + title +"\n");
                }
                

                if (name == "OUTLOOK" & title.Contains(searchTitle))
                {
                    found = true;
                    timerCheckOutlook.Interval = 10000;
                    
                    if (alert.isShown == false) 
                    {
                        alert.Show();
                        alert.BringToFront();
                        alert.WindowState = FormWindowState.Normal;
                        //alert.WindowState = FormWindowState.Maximized;
                        //logBox.AppendText(name + "\n");
                    }
                }

            }

            if (found == false)
            {
                alert.doClose();
                timerCheckOutlook.Interval = 30000;
            }

        }

        public void sendEmails(string subject,float neckarpegel, string email_config_file)
        {
            string[] lines;
            string[] separators = new string[] { "\r\n" };

            if(File.Exists(email_config_file)==false)
            {
                tbStatus.Text = "Error " + email_config_file + " not found";
                return;
            }

            string content = File.ReadAllText(email_config_file);
            lines = content.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            for (int k = 0; k < lines.Length; k++)
            {
                mail.sentOutlookMail(lines[k], subject , "Gundelsheim", neckarpegel);
                mail_done = true;
            }

        }

        public void verifyWaterLevel()

        {
            float neckarpegel = 0;

            neckarpegel = pegel.getWaterLevel("Gundelsheim UP");
            lbPegel.Text = neckarpegel.ToString();

            if (neckarpegel > neckar_pegel_warning)
            {
                NeckarAlarm.tbAlarmText.Text = "Der Pegel in Gundelsheim beträgt " + neckarpegel.ToString() + " cm";
                NeckarAlarm.Show();
                sendEmails("Nackar Pegel Warning", neckarpegel, "warning.txt");
                time_intervall = 30;
            }
            else time_intervall = time_default_intervall;

            if (neckarpegel > neckar_alarm_pegel)
            {
                NeckarAlarm.tbAlarmText.Text = "ALARM : Der in Gundelsheim beträgt " + neckarpegel.ToString() + " cm";
                NeckarAlarm.Show();
                sendEmails("Neckar Pegel Alarm", neckarpegel, "alarm.txt");
                time_intervall = 30;
            }
        }

        public void checkWaterLevel()
        {

            DateTime dt = DateTime.Now;

            searchTitle = OutlookReminder.Properties.Settings1.Default.ReminderTitle;
            neckar_pegel_warning = OutlookReminder.Properties.Settings1.Default.WarningLevel;
            neckar_alarm_pegel = OutlookReminder.Properties.Settings1.Default.Alarmlevel;

            old_time = time;
            time = 60 * dt.Hour + dt.Minute;

            lbMinute.Text = dt.Minute.ToString();

            if(((time % time_intervall) == 0) && (mail_done == false))
            {
                verifyWaterLevel();
            }

            if ((time % time_intervall) == 1) mail_done = false;
           
        }

        /// <summary>
        /// show alerter window if outlook reminder window exists
        /// </summary>

        private void checkReminderWindow()
        {
        	Boolean rc = false;
     	
            if(cbPause.Checked)
            {
                if (alert.isShown) alert.Close();

                pause_counter++;
                if (pause_counter > 25)
                {
                    cbPause.Checked = false;
                    pause_counter = 0;
                }

                return;
            }


        	rc = User32Helper.GetReminderWindow();
        	
	        if (rc==true)
	        {
	            timerCheckOutlook.Interval = 10000;
	            
	            if (alert.isShown == false) 
	            {
	                alert.Show();
	                alert.BringToFront();
	                alert.WindowState = FormWindowState.Normal;
                    alert.isShown = true;
	            }
	        }
            
        }

        private void btnCheckNow_Click(object sender, EventArgs e)
        {
            checkReminderWindow();
            lbPegel.Text = "-----";
            Application.DoEvents();
            Thread.Sleep(500);
            verifyWaterLevel();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            checkReminderWindow();
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

            lbPegel.Text = "-----";
            Application.DoEvents();
            Thread.Sleep(500);
            float neckarpegel = 0;
            neckarpegel = pegel.getWaterLevel("Gundelsheim UP");
            lbPegel.Text = neckarpegel.ToString();
        }

        
        void btnSaveSettings_Click(object sender, EventArgs e)
        {
            OutlookReminder.Properties.Settings1.Default.ReminderTitle = tbSearchTitle.Text;
            OutlookReminder.Properties.Settings1.Default.Save();
            searchTitle = OutlookReminder.Properties.Settings1.Default.ReminderTitle;

            if (cBStartOnLogin.Checked == true)
            {
                setRegistry();
            }
        }

        private void timerFlash_Tick(object sender, EventArgs e)
        {

        }

        private void timerPegel_Tick(object sender, EventArgs e)
        {
            checkWaterLevel();
        }

        private void cbPause_CheckedChanged(object sender, EventArgs e)
        {
            if(cbPause.Checked)
            {
                if (alert.isShown) alert.Close();
            }

        }
    }

public class DesktopWindow
{
    public IntPtr Handle { get; set; }
    public string Title { get; set; }
    public bool IsVisible { get; set; }
}

public class User32Helper
{
    public delegate bool EnumDelegate(IntPtr hWnd, int lParam);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll", EntryPoint = "GetWindowText",
        ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpWindowText, int nMaxCount);

    [DllImport("user32.dll", EntryPoint = "EnumDesktopWindows",
        ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
    public static extern bool EnumDesktopWindows(IntPtr hDesktop, EnumDelegate lpEnumCallbackFunction,
        IntPtr lParam);

    public static Boolean GetReminderWindow()
    {
        Boolean rc = false;
        string title = "";
        
        EnumDelegate filter = delegate(IntPtr hWnd, int lParam)
        {
            var result = new StringBuilder(255);
            GetWindowText(hWnd, result, result.Capacity + 1);
            title = result.ToString();

            var isVisible = !string.IsNullOrEmpty(title) && IsWindowVisible(hWnd);

            if (title.Contains("Reminder(s)") & isVisible == true)
                {
                	rc = true;
                }                                

            return true;
        };

        EnumDesktopWindows(IntPtr.Zero, filter, IntPtr.Zero);
        return rc;
    }
}    
    
}
