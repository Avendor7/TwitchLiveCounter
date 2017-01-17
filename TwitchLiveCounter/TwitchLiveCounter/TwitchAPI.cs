using System;
using System.Collections.Generic;
using System.Windows.Forms;
using RestSharp;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TwitchLiveCounter {
     class TwitchAPI {
        

        public static Object getLiveStatus(string strCommaSepUsernames) {


            var client = new RestClient();
            client.BaseUrl = new Uri("https://api.twitch.tv/kraken/streams?channel=" + strCommaSepUsernames);


            var request = new RestRequest();
            request.Method = Method.GET;
            request.AddHeader("Client-ID", "dsf248t4b6aririduqsh94h9ypzrb0i");
            request.AddHeader("accept", "application/vnd.twitchtv.v3+json");
            request.RequestFormat = DataFormat.Json;

            IRestResponse response = client.Execute(request);

            return JsonConvert.DeserializeObject<RootObject>(response.Content);
            
        }
        public static string helloworld() {

            return "helloworld";
        }
        public static Object getTwitchUsers(string strUsername) {
            //followerListView.Clear();
            //Usernames.Width = followerListView.Size.Width - 21;

           // twitchUsername = twitchUsernameTextBox.Text;

            //create rest client
            var client = new RestClient();
            client.BaseUrl = new Uri("https://api.twitch.tv/kraken/users/");
            //create request
            var request = new RestRequest();
            request.Method = Method.GET;
            request.Resource = strUsername + "/follows/channels?limit=100";
            request.AddHeader("Client-ID", "dsf248t4b6aririduqsh94h9ypzrb0i");
            request.AddHeader("accept", "application/vnd.twitchtv.v3+json");
            request.RequestFormat = DataFormat.Json;

            //execute request
            IRestResponse response = client.Execute(request);

            //parse returned content
            return JsonConvert.DeserializeObject<Rootobject>(response.Content);

            
        }
        
    }
    [Serializable()]
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
