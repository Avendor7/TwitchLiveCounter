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
 * 1) fix the clearing of the lists
 * 2) parse the data returned from Twitch
 * 3) figure out if it is better to grab totals from twitch API or just use the count from the listview
 * 4) see 3, might be easier to also refacter code so the listview is populated from another list when needed
 */


namespace TwitchLiveCounter
{
    public partial class TwitchLiveCounter : Form
    {

        private string twitchUsername = "Avendor7";
        private string usernameString = "";
        
        public TwitchLiveCounter(){
            InitializeComponent();
            
        }

        private void TwitchLiveCounter_Load(object sender, EventArgs e) {

            //load all of the settings from persistant storage

            //username
            if (Properties.Settings.Default.twitchUsernameSettings != null) {
                twitchUsernameTextBox.Text = Properties.Settings.Default.twitchUsernameSettings;
            }
            //followed accounts
            if (Properties.Settings.Default.followedUsersSettingsCollection != null) {
                StringCollection collection = new StringCollection();
                collection = Properties.Settings.Default.followedUsersSettingsCollection;
                List<string> followedList = collection.Cast<string>().ToList();

                foreach (var item in followedList) {
                    followerListView.Items.Add(item);
                }
                Usernames.Width = followerListView.Size.Width - 21;
            }
            //number of followed accounts
            if (Properties.Settings.Default.numberOfFollowedUsers != null) {
                totalFollowersLabel.Text = "Total Followers: " + Properties.Settings.Default.numberOfFollowedUsers;
            }

        }
        private void getLiveStatus() {

            var client = new RestClient();
            client.BaseUrl = new Uri("https://api.twitch.tv/kraken/streams?channel=");

            var request = new RestRequest();
            request.Method = Method.GET;
            request.Resource = usernameString + "?limit=100";
            request.AddHeader("Client-ID", "dsf248t4b6aririduqsh94h9ypzrb0i");
            request.AddHeader("accept", "application/vnd.twitchtv.v3+json");
            request.RequestFormat = DataFormat.Json;

            IRestResponse response = client.Execute(request);

            // Console.WriteLine(response.Content);

            dynamic results = JsonConvert.DeserializeObject<dynamic>(response.Content);

            string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            // Append text to an existing file named "WriteLines.txt".
            using (StreamWriter outputFile = new StreamWriter(mydocpath + @"\WriteLines.txt", true)) {
                outputFile.WriteLine(response.Content);
            }

        }

        private void button2_Click(object sender, EventArgs e) {
            getLiveStatus();
        }

        private void label1_Click(object sender, EventArgs e) {

        }

        private void textBox2_TextChanged(object sender, EventArgs e) {

        }

        private void label3_Click(object sender, EventArgs e) {

        }

        private void groupBox1_Enter(object sender, EventArgs e) {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void getTwitchUsers_Click(object sender, EventArgs e) {

            //followerListView.Clear();
            Usernames.Width = followerListView.Size.Width - 21;

           
            twitchUsername = twitchUsernameTextBox.Text;

            var client = new RestClient();
            client.BaseUrl = new Uri("https://api.twitch.tv/kraken/users/");

            var request = new RestRequest();
            request.Method = Method.GET;
            request.Resource = twitchUsername + "/follows/channels?limit=100";
            request.AddHeader("Client-ID", "dsf248t4b6aririduqsh94h9ypzrb0i");
            request.AddHeader("accept", "application/vnd.twitchtv.v3+json");
            request.RequestFormat = DataFormat.Json;
          
            IRestResponse response = client.Execute(request);
            
            var rootObj = JsonConvert.DeserializeObject<Rootobject>(response.Content);

            foreach (var row in rootObj.follows) {
                followerListView.Items.Add(row.channel.display_name);
                usernameString += row.channel.display_name + ",";
            }
            totalFollowersLabel.Text = "Total Followers: " + rootObj._total.ToString();
            

            var followerList = new List<string>();
            foreach (ListViewItem Item in followerListView.Items) {
                followerList.Add(Item.Text.ToString());
            }

            //create string collection from list of strings
            StringCollection collection = new StringCollection();
            collection.AddRange(followerList.ToArray());

            //save settings to Settings.settings
            Properties.Settings.Default.followedUsersSettingsCollection = collection;
            Properties.Settings.Default.numberOfFollowedUsers = rootObj._total.ToString();
            Properties.Settings.Default.twitchUsernameSettings = twitchUsername;
            Properties.Settings.Default.Save();
            
        }

        private void twitchUsernameTextBox_TextChanged(object sender, EventArgs e) {

        }
    }

    //twitch classes

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


}
