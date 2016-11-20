using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using RestSharp;
using Newtonsoft.Json;
using System.IO;
using System.Collections.Specialized;

/*
 * TODO
 * 1) create installer / uninstaller
 * 2) dynamically change system icon
 * 3) dynamically add items to the context list
 * 4) 
 */


namespace TwitchLiveCounter
{
    public partial class TwitchLiveCounter : Form {

        private string twitchUsername = "Avendor7";
        private List<string> twitchUsers = new List<string> { };
        private List<LiveStatus> currentLive = new List<LiveStatus> { };
        private int timerInterval = 300000;

        public TwitchLiveCounter(){
            InitializeComponent();
            
        }

        private void TwitchLiveCounter_Load(object sender, EventArgs e) {

            //start the timer
            updateTimer.Start();

            //load all of the settings from persistant storage

            //username
            if (Properties.Settings.Default.twitchUsernameSettings != null) {
                twitchUsernameTextBox.Text = Properties.Settings.Default.twitchUsernameSettings;
            }
            //followed accounts
            if (Properties.Settings.Default.followedUsersSettingsCollection != null) {
                StringCollection collection = new StringCollection();
                collection = Properties.Settings.Default.followedUsersSettingsCollection;

                //load saved string and make it a list
                twitchUsers = collection.Cast<string>().ToList();

                //repopulate the listview 
                foreach (var item in twitchUsers) {
                    followerListView.Items.Add(item);
                    totalFollowersLabel.Text = "Total Followers: " + followerListView.Items.Count;

                }

                //reset the width of the listview
                Usernames.Width = followerListView.Size.Width - 21;
            }
            //number of followed accounts
            if (Properties.Settings.Default.numberOfFollowedUsers != null) {
                totalFollowersLabel.Text = "Total Followers: " + Properties.Settings.Default.numberOfFollowedUsers;
            }
            
            //Console.WriteLine(generateUsernameStringDelimited());
            notifyIcon.Icon = System.Drawing.SystemIcons.Application;
            notifyIcon.BalloonTipText = "woo notify";
           // notifyIcon.Text = "Currently Live\n" + ;
            //notifyIcon.ShowBalloonTip(100);


        }

        private void getLiveStatus() {

            
            var client = new RestClient();
            client.BaseUrl = new Uri("https://api.twitch.tv/kraken/streams?channel=" + generateUsernameString());

            //usernameString += "PyrionFlax";

            var request = new RestRequest();
            request.Method = Method.GET;
            request.AddHeader("Client-ID", "dsf248t4b6aririduqsh94h9ypzrb0i");
            request.AddHeader("accept", "application/vnd.twitchtv.v3+json");
            request.RequestFormat = DataFormat.Json;

            IRestResponse response = client.Execute(request);


            var rootObj = JsonConvert.DeserializeObject<RootObject>(response.Content);

            if (rootObj._total == 0) {
                Console.WriteLine("Its Null Jim");
                //also delete everyone from the list
            }
            else {

                List<LiveStatus> updatedList = new List<LiveStatus> { };

                

                foreach (var row in rootObj.streams) {

                   updatedList.Add(new LiveStatus() { user = row.channel.display_name, game = row.channel.game, status = row.channel.status, viewers = row.viewers });

                    if (!currentLiveListCheck(row.channel.display_name)) {
                        //check if now live, and not in the list
                        currentLive.Add(new LiveStatus() { user = row.channel.display_name, game = row.channel.game, status = row.channel.status, viewers = row.viewers });
                        Console.Write(row.channel.display_name);
                        Console.WriteLine(": Added to List");
                    }else if(currentLiveListCheck(row.channel.display_name)) {
                        //still live
                        Console.Write(row.channel.display_name);
                        Console.WriteLine(": Still live");
                    }
                  //  currentLive.Add(new LiveStatus() { user = row.channel.display_name, game = row.channel.game, status = row.channel.status, viewers = row.viewers });
                    //Console.WriteLine(currentLive.Count());
                   

                }
                
            }

            //do the deleting things
               // foreach (var currentlyLiveUser in currentLive) {
               //     if (!user.Contains(currentlyLiveUser.user)) {
               //         Console.Write(user);
               //         Console.WriteLine(": deleted");
               //     }
               // }

            //notifyIcon.Text = "Currently Live\n" +  + "\n";

        }
       
        private bool currentLiveListCheck(string userToCheck) {
            //check for still streaming
            foreach (var liveUsers in currentLive) {
                if (liveUsers.user == userToCheck) {
                    return true;
                }
            }
            //if nothing is returned at this point then add to list
            return false;
        } 
        private bool deleteCheck() {

            return false;
        }


        private void button2_Click(object sender, EventArgs e) {
            getLiveStatus();
        }
        
        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
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

            //loop through follows
            foreach (var row in rootObj.follows) {
                twitchUsers.Add(row.channel.display_name);
            }
            
            //add 
            //var followerList = new List<string>();
           // foreach (ListViewItem Item in followerListView.Items) {
           //     followerList.Add(Item.Text.ToString());
          //  }
            //add users from the twitchUsers list to the listview to be displayed to the user
            foreach (var Item in twitchUsers) {
                followerListView.Items.Add(Item);
                totalFollowersLabel.Text = "Total Followers: " + followerListView.Items.Count;
            }
            
            //create string collection from list of strings
            StringCollection collection = new StringCollection();
            collection.AddRange(twitchUsers.ToArray());

            //save settings to Settings.settings
            Properties.Settings.Default.timerInterval = timerInterval;
            Properties.Settings.Default.followedUsersSettingsCollection = collection;
            Properties.Settings.Default.numberOfFollowedUsers = rootObj._total.ToString();
            Properties.Settings.Default.twitchUsernameSettings = twitchUsername;
            
        }

        //pass in the list, get a really not fancy string back with commas
        private string generateUsernameString() {
            string usernameString = "";
            foreach (var user in twitchUsers) {
                usernameString += user + ",";
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
            //TwitchLiveCounter.ActiveForm.WindowState = FormWindowState.Normal;
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
            //default 5
            
            try {
                timerInterval = Int32.Parse(textBox2.Text);
            }
            catch (FormatException f) {
                Console.WriteLine(f.Message);
            }
            //update the timer to the specified interval converting minutes to miliseconds
            updateTimer.Interval = timerInterval * 60000;
        }
    }

    //twitch GET /users/:user/follows/channels


    public class LiveStatus {
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
