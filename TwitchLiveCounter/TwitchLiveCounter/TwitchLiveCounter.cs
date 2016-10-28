using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;
using Newtonsoft.Json;

namespace TwitchLiveCounter
{
    public partial class TwitchLiveCounter : Form
    {
        public TwitchLiveCounter()
        {
            InitializeComponent();
        }

        private void TwitchLiveCounter_Load(object sender, EventArgs e)
        {
            var client = new RestClient();
            client.BaseUrl = new Uri("https://api.twitch.tv/kraken/streams/");

            string[] streamer = new string[3] { "katgunn", "esl_overwatch", "dexteritybonus" };

            for (int i =0; i<=2; i++) {
                var request = new RestRequest();
                request.Method = Method.GET;
                request.Resource = streamer[i];
                request.AddHeader("Client-ID", "dsf248t4b6aririduqsh94h9ypzrb0i");
                request.AddHeader("accept", "application/vnd.twitchtv.v3+json");
                request.RequestFormat = DataFormat.Json;

                IRestResponse response = client.Execute(request);

                // Console.WriteLine(response.Content);

                dynamic results = JsonConvert.DeserializeObject<dynamic>(response.Content);
                var stream = results.stream;

                if (stream != null) {
                    Console.WriteLine(streamer[i] + " is Live");
                }
                else {
                    Console.WriteLine(streamer[i] + " is NOT Live");
                }

            }
            

            //Console.WriteLine(stream);
            /*
            if (!string.IsNullOrEmpty(results.stream))
            {
                Console.WriteLine("Streamer is Live");
            }
            */

        }


       

        }
}
