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
    [Channel("Safrit22")]
    [User("any")]
    public class OffLife : Controller
    {
        private readonly JsonHelper _helper;
        private readonly APIHelper _api;
        public OffLife(JsonHelper helper, APIHelper api)
        {
            _helper = helper;
            _api = api;
        }
        [StartWith("!сервер")]
        public void Server(User u) {
            u = u == null ? _api.User : u;
            SendAnswer("OffLife - приватный сервер по майну, что бы попасть туда, нужно подружится с чатиком PetTheWoah", u.UserName);
        }
        [StartWith("!server")]
        public void Server2(User u) {
            u = u == null ? _api.User : u;
            SendAnswer("OffLife - приватный сервер по майну, что бы попасть туда, нужно подружится с чатиком PetTheWoah", u.UserName);
        }
        [StartWith("!offlife")]
        public void OffLifea(User u) {
            u = u == null ? _api.User : u;
            SendAnswer("OffLife - приватный сервер по майну, что бы попасть туда, нужно подружится с чатиком PetTheWoah", u.UserName);
        }
        

    }

}
