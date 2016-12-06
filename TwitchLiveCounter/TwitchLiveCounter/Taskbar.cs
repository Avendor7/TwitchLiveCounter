using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwitchLiveCounter {
    public partial class Taskbar : Form {
        public Taskbar() {
            InitializeComponent();
        }

        //create new settings form
        TwitchLiveCounter twitchLiveCounter = new TwitchLiveCounter();

        private void Taskbar_Load(object sender, EventArgs e) {
            
            //hide the settings form
            twitchLiveCounter.Hide();

            //set form position to bottom of the screen
            SetFormPosition();
        }

        private void btnSettings_Click(object sender, EventArgs e) {
            //show settings form when button clicked
            twitchLiveCounter.Show();
        }

        private void btnUpdateNow_Click(object sender, EventArgs e) {

        }

        private void closeform(object sender, EventArgs e) {
            //uncomment to close the form when focus on the form is lost
            this.Close();
        }

        private void SetFormPosition() {
            int leftpos = 0;
            int toppos = 0;
            leftpos = (Screen.PrimaryScreen.WorkingArea.Right - 2) - Width;
            toppos = (Screen.PrimaryScreen.WorkingArea.Bottom - 2) - Height;
            this.Location = new Point(leftpos, toppos);
        }

        private void tmrUpdate_Tick(object sender, EventArgs e) {
            
            
        }
    }
}
