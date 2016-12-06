using System;
using System.Collections.Generic;
using System.Windows.Forms;
using RestSharp;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TwitchLiveCounter {
    class TwitchAPI {
        private static TwitchAPI instance;

        private TwitchAPI() { }

        public static TwitchAPI Instance {
            get {
                if (instance == null) {
                    instance = new TwitchAPI();
                }
                return instance;
            }
        }

        public void getLiveStatus() {


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

    }
}
