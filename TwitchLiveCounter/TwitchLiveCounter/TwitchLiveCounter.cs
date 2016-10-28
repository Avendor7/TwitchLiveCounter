﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RestSharp;

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
           

            var request = new RestRequest();
            request.Method = Method.GET;
            request.Resource = "KatGunn";
            request.AddHeader("Client-ID", "dsf248t4b6aririduqsh94h9ypzrb0i");
            request.AddHeader("accept", "application/vnd.twitchtv.v3+json");
            request.RequestFormat = DataFormat.Json;

            IRestResponse response = client.Execute(request);
           
            Console.Write(response.Content);


        }


       

        }
}
