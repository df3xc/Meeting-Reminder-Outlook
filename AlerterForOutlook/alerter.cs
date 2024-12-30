using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OutlookReminder
{
    public partial class alerter : Form
    {

        private int timerTicks = 0;

        public Boolean isShown = false;

        public alerter()
        {
            InitializeComponent();
        }

        public void doClose()
        {
            this.Hide();
            isShown = false;
        }

        private void alerter_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            isShown = false;
        }

        private void alerter_Shown(object sender, EventArgs e)
        {
            isShown = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if ((timerTicks % 4) == 0)
            {
                this.BackColor = Color.Orange;
            }
            else
            {
                this.BackColor = Color.Red;

            }
            timerTicks++;

        }

        private void alerter_Load(object sender, EventArgs e)
        {
            Point location = new Point();
            location = Screen.AllScreens[0].WorkingArea.Location;

            if (Screen.AllScreens.Length > 1)    // show on second monitor
            {
                location = Screen.AllScreens[1].WorkingArea.Location;
            }

            location.X = location.X + 70;
            location.Y = location.Y + 33;
            this.Location = location;
        }
    }
}
