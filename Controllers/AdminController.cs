using System;
using System.Collections.Generic;
using Twitcher;
using TwitchLib.Api;
using TwitchLib.Api.Helix.Models.ChannelPoints;
using System.Linq;
using Twitcher.Controllers;
using Twitcher.Controllers.Attributes;
using Twitcher.Controllers.JsonHelper;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Api.Helix.Models.ChannelPoints.CreateCustomReward;
using LiphiBot2.Models;
using Twitcher.Controllers.APIHelper;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.CSharp.Scripting;

namespace LiphiBot2.Controllers
{
    [Channel("any")]
    [User("LiphiTC")]
    [User("LiphiTheCat")]
    [User("33KK")]
    public class AdminController : Controller
    {
        private readonly JsonHelper _helper;
        private readonly APIHelper _api;
        public AdminController(JsonHelper helper, APIHelper api)
        {
            _helper = helper;
            _api = api;
        }

        [StartWith("!ebal")]
        public async void Ebal()
        {
            if (Message.Message.Length <= 6)
            {
                return;
            }

            string expr = Message.Message.Substring(6);

            try
            {
                var result = await CSharpScript.EvaluateAsync(expr,
                    ScriptOptions.Default
                        .WithImports(new string[]
                        {
                            "System"
                        })
                        .WithEmitDebugInformation(true)
                );

                if (result != null)
                {
                    SendAnswer(result.ToString());
                }
                else
                {
                    SendAnswer("А нихуя Starege");
                }
            }
            catch (Exception e)
            {
                SendAnswer($"Пиздец Starege {e.Message}");
            }
        }

        [StartWith("!addcomb")]
        public void AddComb()
        {
            string[] splited = Message.Message.Split(' ');
            string s1 = splited[1];
            string s2 = splited[2];
            var s = _helper.GetObject<Dictionary<string, string>>("SmilesSpamer", "SpamOn");
            if (s is null)
                s = new();
            if (s.Any(x => x.Key == (s1 + ' ' + s2)))
            {
                SendAnswer("такая комбинация уже есть WoahBlanket");
                return;
            }

            s.Add(s1 + ' ' + s2, s1 + ' ' + s2);
            SafritController.smileSpam.Add(s1 + ' ' + s2, s1 + ' ' + s2);
            _helper.EditObject<Dictionary<string, string>>("SmilesSpamer", "SpamOn", s);
            SendAnswer("готово WoahBlanket");
        }
        [StartWith("!addtimezone")]
        public void AddTimeZone(string zone, int? time)
        {

            var s = _helper.GetObject<Dictionary<string, int>>("TimeZones", "Zones");
            if (s is null)
                s = new();
            if (zone is null || time is null)
            {
                SendAnswer("Чё-та хуйня NOPE");
                return;
            }
            s.Add(zone, time.Value);
            _helper.EditObject<Dictionary<string, int>>("TimeZones", "Zones", s);
            SendAnswer("готово WoahBlanket");
        }
        [StartWith("!settimezone")]
        public void SetTimeZone(User user, string time)
        {

            var s = _helper.GetObject<Dictionary<string, int>>("TimeZones", "Zones");
            if (s is null)
                s = new();
            if (user is null || time is null)
            {
                SendAnswer("Чё-та хуйня NOPE");
                return;
            }
            _helper.EditObject<Dictionary<string, int>>("TimeZones", "Zones", s);
            SendAnswer("готово WoahBlanket");
        }
        [CoolDown(50)]
        [StartWith("!чатеры")]
        public async void Chatter()
        {
            var usersNotParsed = await _api.API.Undocumented.GetChattersAsync(_api.Channel.Broadcaster.UserName);
            var chatters = await _api.Channel.GetUsersAsync();
            SendAnswer("WoahBlanket 👉 " + chatters.Count);
        }
        private static string _timerName;
        private static DateTime _timerStart;
        [StartWith("!starttimer")]
        public void StartTimer()
        {
            string name = Message.Message.Substring(11);
            if (_timerStart != default(DateTime))
            {
                SendAnswer("Уже идёт таймер YEP");
                return;
            }
            _timerName = name;
            _timerStart = DateTime.Now;
            SendAnswer("Таймер пошёл PETTHEPEPEGA");
        }
        [StartWith("!stoptimer")]
        public void StopTimer()
        {
            if (_timerStart == null)
            {
                SendAnswer("Нету таймера YEP");
                return;
            }
            SendAnswer("Таймер отсановлен PETTHEPEPEGA на " + _timerName + " ушло " + (DateTime.Now - _timerStart) + " PETTHEPEPEGA");
            _timerStart = default;
        }
        [StartWith("!asslogin")]
        public void AssLogin()
        {
            SendAnswer("Успешный вход в жопу YEP");
        }
        [StartWith("!registerreward")]
        public async void RegisterReward()
        {
            string[] splited = UnionString(Message.Message.Split(" "));
            string asToken = "MAIN_REWARD";
            CreateCustomRewardsRequest rewardsRequest = new();
            for (int i = 0; i < splited.Length; i++)
            {
                switch (splited[i])
                {
                    case "as":
                        if (i + 1 != splited.Length)
                            asToken = splited[i + 1];
                        break;
                    case "cost":
                        int cost;
                        if (i + 1 != splited.Length)
                        {
                            if (int.TryParse(splited[i + 1], out cost))
                            {
                                rewardsRequest.Cost = cost;
                                break;
                            }
                            SendAnswer("maxPreStream должно быть числом PETTHEPEPEGA");
                            return;
                        }
                        break;
                    case "maxPreStream":
                        int maxPreStreamCount;
                        if (i + 1 != splited.Length)
                        {
                            if (int.TryParse(splited[i + 1], out maxPreStreamCount))
                            {
                                rewardsRequest.IsMaxPerStreamEnabled = true;
                                rewardsRequest.MaxPerStream = maxPreStreamCount;
                                break;
                            }
                            SendAnswer("maxPreStream должно быть числом PETTHEPEPEGA");
                            return;
                        }
                        break;
                    case "name":
                        if (i + 1 != splited.Length)
                            rewardsRequest.Title = splited[i + 1];
                        break;
                }
            }
            TokenInfo apiToken = Program.Tokens.FirstOrDefault(x => x.TokenPurpose == asToken);
            if (apiToken == null)
            {
                SendAnswer($"Не найден токен {asToken} PETTHEPEPEGA");
                return;
            }
            TwitchAPI api = new TwitchAPI();
            api.Settings.AccessToken = apiToken.Token;
            api.Settings.ClientId = apiToken.ClientID;

            var g = new User(api, apiToken.UserName, CreateType.ByUserName).UserID.ToString();
            ;
            var m = await api.Helix.ChannelPoints.CreateCustomRewards(g.ToString(), rewardsRequest);
            ;
        }

        private string[] UnionString(string[] input)
        {
            bool unionMode = false;
            List<string> result = new();
            string unionedString = "";
            foreach (string s in input)
            {
                if (unionMode)
                {
                    unionedString += (' ' + s);
                    if (unionedString.Last() == '\"')
                    {
                        unionedString = unionedString[..^1];
                        result.Add(unionedString);
                        unionMode = false;
                    }
                    continue;
                }
                if (s[0] == '\"')
                {
                    unionMode = true;
                    unionedString = s.Substring(1);
                    continue;
                }
                result.Add(s);
            }
            return result.ToArray();
        }


    }
}
