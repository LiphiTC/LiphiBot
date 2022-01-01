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
    [Channel("Toni__Stark_")]
    [User("any")]
    public class EblanController : Controller
    {
        private static Dictionary<string, DateTime> userCooldown = new();
        private static List<Duel> activeDuel = new();
        private readonly JsonHelper _helper;
        private readonly APIHelper _api;
        public EblanController(JsonHelper helper, APIHelper api)
        {
            _helper = helper;
            _api = api;
        }
        [StartWith("!дуэльвоах")]
        public void Duel(User u)
        {
            string[] splited = Message.Message.Split(' ');
            if (u != null)
            {
                if (activeDuel.Any(x => x.User1 == Message.Username))
                {
                    SendAnswer("У вас уже есть активная дуэль YEP Clap");
                    return;
                }
                if (activeDuel.Any(x => x.User2 == u.UserName))
                {
                    SendAnswer("У этого пользователя уже есть активная дуэль YEP Clap");
                    return;
                }
                var duel = GenerateDuel(Message.Username, u.UserName);
                activeDuel.Add(duel);
                SendAnswer($"Вас вызвал на дуэль {Message.DisplayName}. Что бы принять напишите !дуэльофф принять", u.UserName);
            }
            if(splited.Length < 2) {
                SendAnswer("Укажите параметр: принять | отмена | топ | {ник}");
                return;
            }
            if(splited[1] == "принять") {
                var duel = activeDuel.FirstOrDefault(x => x.User2 == Message.Username);
                if(duel == null) {
                    SendAnswer("Вас никто не вызвал на дуэль Sadge");
                    return;
                }
                SendAnswer($"@{duel.User2}, посчитайте количество '|', {duel.Key}");
                return;
            }
        }

        private Duel GenerateDuel(string user1, string user2)
        {
            Random r = new Random();
            int answer = r.Next(10, 20);
            int createdAnswers = 0;
            string key = "";
            while (createdAnswers != answer)
            {
                Random r1 = new Random();
                int probability = r.Next(0, 10);
                if (probability == 3)
                {
                    key += "|";
                    createdAnswers++;
                    continue;
                }
                key += ".";
            }
            return new Duel()
            {
                User1 = user1,
                User2 = user2,
                Answer = answer,
                Key = key
            };
        }


    }

    public class Duel
    {
        public string User1 { get; init; }
        public string User2 { get; init; }
        public string Key { get; init; }
        public int Answer { get; init; }
    }

}
