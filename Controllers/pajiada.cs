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
    [Channel("pajlada")]
    
    public class Pajiada : Controller
    {

        [Contains("ALERT")]
        [User("pajbot")]
        [User("LiphiTC")]
        public void Pizda()
        {
            Send("/me PepeS 🚨 ПИЗДЕЦ");
        }



    }

}
