using System;
using System.Collections.Generic;
using System.Windows.Forms;
using RestSharp;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TwitchLiveCounter {
    public partial class TwitchLiveCounter : Form {

        private string twitchUsername = "Avendor7";
        private List<string> twitchUsers = new List<string> { };
        private List<UserList> masterUserList = new List<UserList> { };
        private List<UserList> updatedUserList = new List<UserList> { };

        private int timerInterval = 300000;

        public TwitchLiveCounter() {
            InitializeComponent();

        }

        private void TwitchLiveCounter_Load(object sender, EventArgs e) {
            //start the timer
            if (Properties.Settings.Default.timerInterval > 100) {
                updateTimer.Interval = Properties.Settings.Default.timerInterval;
            }else {
                updateTimer.Interval = timerInterval;
            }
            
            updateTimer.Start();

            //load all of the settings from persistant storage

            //username
            if (Properties.Settings.Default.twitchUsernameSettings != null) {
                twitchUsernameTextBox.Text = Properties.Settings.Default.twitchUsernameSettings;
            }
            //followed accounts with master list

            if (Properties.Settings.Default.userList != "") {
                //load in the master list
                masterUserList = LoadUsers();

                //repopulate the listview 
                foreach (var Item in masterUserList) {
                    followerListView.Items.Add(Item.user);
                    totalFollowersLabel.Text = "Total Followers: " + followerListView.Items.Count;
                }

                //reset the width of the listview
                Usernames.Width = followerListView.Size.Width - 21;
            }
            //Console.WriteLine(generateUsernameStringDelimited());
            notifyIcon.Icon = System.Drawing.SystemIcons.Application;

            notifyIcon.Text = "Twitch Live Counter";

            //updateTimer_Tick(null, EventArgs.Empty);
        }

        private void getLiveStatus() {


            var client = new RestClient();
            client.BaseUrl = new Uri("https://api.twitch.tv/kraken/streams?channel=" + generateUsernameString());
            

            var request = new RestRequest();
            request.Method = Method.GET;
            request.AddHeader("Client-ID", "dsf248t4b6aririduqsh94h9ypzrb0i");
            request.AddHeader("accept", "application/vnd.twitchtv.v3+json");
            request.RequestFormat = DataFormat.Json;

            IRestResponse response = client.Execute(request);

            var rootObj = JsonConvert.DeserializeObject<RootObject>(response.Content);

            

            if (rootObj._total == 0) {
                //check offline
                //TODO have a message if the last person goes offline with display_name and that there is now no one online
                checkOffline();
                notifyIcon.BalloonTipText = "No one is live";
                notifyIcon.ShowBalloonTip(100);
            }
            else {
                foreach (var row in rootObj.streams) {
                    //add users to the updated list
                    updatedUserList.Add(new UserList() { user = row.channel.display_name, game = row.channel.game, status = row.channel.status, viewers = row.viewers, live = true });
                    
                }
                //do all of the checks and clear the list
                checkLive();
                checkOffline();
                updatedUserList.Clear();
            }
            
        }
        
        private void checkOffline() {
            foreach (var streamer in masterUserList) {
                if (streamer.live == true && !updatedUserList.Exists(e => e.user == streamer.user)) {
                    streamer.live = false;
                    notifyIcon.BalloonTipText = streamer.user + " is now offline";
                    notifyIcon.ShowBalloonTip(100);
                }
            }
        }

        private void checkLive() {
            //set everyone who is live to live, it will just overwrite anyone who is already live, plus include updated information
            foreach (var streamer in updatedUserList) {
                var index = masterUserList.FindIndex(a => a.user == streamer.user);
                //update the entire object
                if (!masterUserList[index].live == true) {
                    notifyIcon.BalloonTipText = streamer.user + " is now live";
                    notifyIcon.ShowBalloonTip(100);
                }
                masterUserList[index] = streamer;
            }
        }
        
        private void button2_Click(object sender, EventArgs e) {
            getLiveStatus();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            notifyIcon.Visible = false;
            Environment.Exit(0);
        }

        private void getTwitchUsers_Click(object sender, EventArgs e) {

            //followerListView.Clear();
            Usernames.Width = followerListView.Size.Width - 21;
            
            twitchUsername = twitchUsernameTextBox.Text;

            //create rest client
            var client = new RestClient();
            client.BaseUrl = new Uri("https://api.twitch.tv/kraken/users/");
            //create request
            var request = new RestRequest();
            request.Method = Method.GET;
            request.Resource = twitchUsername + "/follows/channels?limit=100";
            request.AddHeader("Client-ID", "dsf248t4b6aririduqsh94h9ypzrb0i");
            request.AddHeader("accept", "application/vnd.twitchtv.v3+json");
            request.RequestFormat = DataFormat.Json;

            //execute request
            IRestResponse response = client.Execute(request);

            //parse returned content
            var rootObj = JsonConvert.DeserializeObject<Rootobject>(response.Content);

            //loop through follows to the master list
            foreach (var row in rootObj.follows) {
                //twitchUsers.Add(row.channel.display_name);
                masterUserList.Add(new UserList() { user = row.channel.display_name, game = row.channel.game, status = row.channel.status, viewers = 0, live = false });

            }

            //add users from the masteruserlist to the listview to be displayed to the user
            foreach (var Item in masterUserList) {
                followerListView.Items.Add(Item.user);
                totalFollowersLabel.Text = "Total Followers: " + followerListView.Items.Count;
            }

            //save settings to Settings.settings
            saveMasterList(masterUserList);
            Properties.Settings.Default.timerInterval = timerInterval;
            Properties.Settings.Default.twitchUsernameSettings = twitchUsername;

        }

        //pass in the list, get a really not fancy string back with commas
        private string generateUsernameString() {
            string usernameString = "";
            foreach (var user in masterUserList) {
                usernameString += user.user + ",";
            }

            return usernameString;
        }

        private void twitchUsernameTextBox_TextChanged(object sender, EventArgs e) {

        }

        private void clearAllSettingsToolStripMenuItem_Click(object sender, EventArgs e) {

            updateTimer.Interval = timerInterval;
            twitchUsers.Clear();
            followerListView.Items.Clear();
            twitchUsernameTextBox.Clear();
            twitchUsername = "";
            Properties.Settings.Default.Reset();
        }

        //save the username, list of users, and TODO update interval
        private void saveToolStripMenuItem_Click(object sender, EventArgs e) {
            Properties.Settings.Default.Save();
        }

        private void TwitchLiveCounter_FormClosing(Object sender, FormClosingEventArgs e) {

            e.Cancel = true;
            this.Hide();
            notifyIcon.Visible = true;

        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e) {
            notifyIcon.Visible = false;
            Environment.Exit(0);
        }

        private void maximizeToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Show();
        }

        private void updateTimer_Tick(object sender, EventArgs e) {
            getLiveStatus();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e) {

            //validation
            e.Handled = char.IsNumber(e.KeyChar) || (e.KeyChar == 8) ? false : true;
        }

        private void textBox2_Leave(object sender, EventArgs e) {

            //grab the minute value when focus from the textbox is lost, might be a better way of doing this
            try {
                timerInterval = Int32.Parse(textBox2.Text);
            }
            catch (FormatException f) {
                Console.WriteLine(f.Message);
            }
            //update the timer to the specified interval converting minutes to miliseconds
            updateTimer.Interval = timerInterval * 60000;
        }

        void saveMasterList(List<UserList> userlist) {
            using (MemoryStream ms = new MemoryStream()) {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, userlist);
                ms.Position = 0;
                byte[] buffer = new byte[(int)ms.Length];
                ms.Read(buffer, 0, buffer.Length);
                var converted = Convert.ToBase64String(buffer);
                Properties.Settings.Default.userList = converted;
                //Properties.Settings.Default.Save();
            }
        }

        List<UserList> LoadUsers() {
            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(Properties.Settings.Default.userList))) {
                BinaryFormatter bf = new BinaryFormatter();
                return (List<UserList>)bf.Deserialize(ms);
            }
        }
    }

    //twitch GET /users/:user/follows/channels
    
    public class UserList {
        public string user { get; set; }
        public string status { get; set; }
        public string game { get; set; }
        public int viewers { get; set; }
        public bool live { get; set; }

    }

    public class Rootobject {
        public Follow[] follows { get; set; }
        public int _total { get; set; }
        public _Links _links { get; set; }
    }

    public class _Links {
        public string self { get; set; }
        public string next { get; set; }
    }

    public class Follow {
        public DateTime created_at { get; set; }
        public _Links1 _links { get; set; }
        public bool notifications { get; set; }
        public Channel channel { get; set; }
    }

    public class _Links1 {
        public string self { get; set; }
    }

    public class Channel {
        public _Links2 _links { get; set; }
        public object background { get; set; }
        public object banner { get; set; }
        public string broadcaster_language { get; set; }
        public string display_name { get; set; }
        public string game { get; set; }
        public string logo { get; set; }
        //public bool mature { get; set; }
        public string status { get; set; }
        public bool partner { get; set; }
        public string url { get; set; }
        public string video_banner { get; set; }
        public int _id { get; set; }
        public string name { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public object delay { get; set; }
        public int followers { get; set; }
        public string profile_banner { get; set; }
        public string profile_banner_background_color { get; set; }
        public int views { get; set; }
        public string language { get; set; }
        public Links links { get; set; }
    }

    public class _Links2 {
        public string self { get; set; }
        public string follows { get; set; }
        public string commercial { get; set; }
        public string stream_key { get; set; }
        public string chat { get; set; }
        public string subscriptions { get; set; }
        public string editors { get; set; }
        public string videos { get; set; }
        public string teams { get; set; }
    }

    public class Preview {
        public string small { get; set; }
        public string medium { get; set; }
        public string large { get; set; }
        public string template { get; set; }
    }

    public class Links {
        public string self { get; set; }
        public string follows { get; set; }
        public string commercial { get; set; }
        public string stream_key { get; set; }
        public string chat { get; set; }
        public string features { get; set; }
        public string subscriptions { get; set; }
        public string editors { get; set; }
        public string teams { get; set; }
        public string videos { get; set; }
    }

    public class Links2 {
        public string self { get; set; }
    }

    public class Stream {
        public long _id { get; set; }
        public string game { get; set; }
        public int viewers { get; set; }
        public int video_height { get; set; }
        public double average_fps { get; set; }
        public int delay { get; set; }
        public string created_at { get; set; }
        public bool is_playlist { get; set; }
        public Preview preview { get; set; }
        public Channel channel { get; set; }
        public Links2 _links { get; set; }
    }

    public class Links3 {
        public string self { get; set; }
        public string next { get; set; }
        public string featured { get; set; }
        public string summary { get; set; }
        public string followed { get; set; }
    }

    public class RootObject {
        public int _total { get; set; }
        public List<Stream> streams { get; set; }
        public Links3 _links { get; set; }
    }

}
