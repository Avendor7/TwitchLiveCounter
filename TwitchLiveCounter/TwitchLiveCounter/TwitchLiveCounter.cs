﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using RestSharp;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TwitchLiveCounter {
    public partial class TwitchLiveCounter : Form {

        
        
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
        
        
        
        
        
        private void button2_Click(object sender, EventArgs e) {
            TwitchAPI api = TwitchAPI.Instance;
            api.getLiveStatus();
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

    


}
