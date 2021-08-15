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
    [Channel("LiphiTC")]
    [User("any")]
    public class MemController : Controller
    {
        private readonly JsonHelper _helper;
        private readonly APIHelper _api;
        public MemController(JsonHelper helper, APIHelper api)
        {
            _helper = helper;
            _api = api;
        }
        [StartWith("!проект")]
        public void Server(User u)
        {
            u = u == null ? _api.User : u;
            SendAnswer("Кастомный аутх сервер для майна WoahBlanket", u.UserName);
        }

        [StartWith("!дела")]
        public void Server2(User u) {
            u = u == null ? _api.User : u;
            SendAnswer("https://i.imgur.com/eUfNRNA.png YEP", u.UserName);
        }

         [StartWith("!янаместе")]
        public void IaNaMeste(User u) {
            u = u == null ? _api.User : u;
            SendAnswer("https://i.kkx.one/t8iste5i.png YEP ", u.UserName);
        }
        [StartWith("!розыск")]
        public void D(User u) {
            u = u == null ? _api.User : u;
            SendAnswer("https://i.kkx.one/60vylrtv.png Нужно поймать суку D:  ", u.UserName);
        }
        [StartWith("!дрочу")]
        public void Cum(User u) {
            u = u == null ? _api.User : u;
            SendAnswer("https://i.kkx.one/hlfsnpcm.png PETTHEPEPEGA", u.UserName);
        }
        



    }

}
