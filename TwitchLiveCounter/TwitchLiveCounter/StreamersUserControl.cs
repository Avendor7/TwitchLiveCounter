using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwitchLiveCounter {
    public partial class StreamersUserControl : UserControl {
        public StreamersUserControl() {
            InitializeComponent();
        }

        private void StreamersUserControl_Load(object sender, EventArgs e) {

        }

        public string userLabel {
            get { return user.Text; }
            set { user.Text = value; }
        }

        public string gameLabel {
            get { return game.Text; }
            set { game.Text = value; }
        }

        public string viewersLabel {
            get { return viewers.Text; }
            set { viewers.Text = value; }
        }

        public string descriptionLabel {
            get { return description.Text; }
            set { description.Text = value; }
        }
    }
}
