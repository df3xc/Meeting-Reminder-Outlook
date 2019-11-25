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
using System.Runtime.InteropServices;


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
            checkReminderWindow();
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
                logBox.AppendText(name + " : " + title +"\n");                
                if (name == "OUTLOOK")
                {
                	logBox.AppendText(name + " : " + title +"\n");
                }
                

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
                        logBox.AppendText(name + "\n");
                    }
                }

            }

            if (found == false)
            {
                alert.doClose();
                timer1.Interval = 30000;
            }

        }
        
        
        /// <summary>
        /// show alerter window if outlook reminder window exists
        /// </summary>
        
        private void checkReminderWindow()
        {
        	Boolean rc = false;
        	
        	rc = User32Helper.GetReminderWindow();
        	
	        if (rc==true)
	        {
	            timer1.Interval = 10000;
	            
	            if (alert.isShown == false) 
	            {
	                alert.Show();
	                alert.BringToFront();
	                alert.WindowState = FormWindowState.Normal;
	            }
	        }        	
        }

        private void btnCheckNow_Click(object sender, EventArgs e)
        {
            checkReminderWindow();
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
        }

        
        void btnSaveSettings_Click(object sender, EventArgs e)
        {
            OutlookReminder.Properties.Settings1.Default.ReminderTitle = tbSearchTitle.Text;
            OutlookReminder.Properties.Settings1.Default.Save();
            searchTitle = OutlookReminder.Properties.Settings1.Default.ReminderTitle;
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
