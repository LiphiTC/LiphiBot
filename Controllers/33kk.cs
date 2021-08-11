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
    [Channel("33kk")]
    [User("any")]
    public class Three3kk : Controller
    {
        private readonly JsonHelper _helper;
        private readonly APIHelper _api;
        public Three3kk(JsonHelper helper, APIHelper api)
        {
            _helper = helper;
            _api = api;
        }
        [StartWith("!проект")]
        public void Server(User u) {
            u = u == null ? _api.User : u;
            SendAnswer("Кастомный аутх сервер для майна WoahBlanket", u.UserName);
        }
       
        

    }

}
