using System;
using System.Collections.Generic;
using Twitcher;
using System.Linq;
using Twitcher.Controllers;
using Twitcher.Controllers.Attributes;
using Twitcher.Controllers.JsonHelper;
using Cliver;
using Newtonsoft.Json;
using RestSharp;
using LiphiBot2.Models;
using Newtonsoft.Json.Linq;
using Twitcher.Controllers.APIHelper;


namespace LiphiBot2.Controllers
{
    [Channel("Bekert")]
    [Channel("33kk")]

    public class pajaS : Controller
    {

        public pajaS(JsonHelper helper, APIHelper api)
        {

        }
        [StartWith("!news")]
        public async void News()
        {
            Client.Bot.SendMessage("LiphiTC", "$luam");
            Program.NewsRequests.Add(Message.Channel);
        }


    }

}
