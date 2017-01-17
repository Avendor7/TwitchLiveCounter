using System;
using System.Collections.Generic;
using System.Windows.Forms;
using RestSharp;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TwitchLiveCounter {
    class TwitchAPIAccess {

        public string twitchUsername = "Avendor7";
        public List<string> twitchUsers = new List<string> { };
        public List<UserList> masterUserList = new List<UserList> { };
        public List<UserList> updatedUserList = new List<UserList> { };

       // TwitchAPI blah = TwitchAPI.Instance();

        
        
        

        public void loopLiveStatuses() {
            Object rootObj = TwitchAPI.getTwitchUsers("Avendor7");
            var blah = JsonConvert.DeserializeObject<RootObject>(rootObj);

            Console.WriteLine(TwitchAPI.helloworld());
            if (rootObj._total == 0) {
                
                blah._total
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

        //twitch GET /users/:user/follows/channels
        public void loopFollows() {
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
    }
}
