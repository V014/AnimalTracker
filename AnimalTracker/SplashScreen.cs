using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimalTracker
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        Timer time;
        private void SplashScreen_Shown(object sender, EventArgs e)
        {
            time = new Timer();
            time.Interval = 3000;
            time.Start();
            time.Tick += new EventHandler(time_Tick);
        }
        void time_Tick(object sender, EventArgs e)
        {
            time.Stop();
            Home h = new Home();
            h.Show();
            this.Hide();
        }
    }
}
