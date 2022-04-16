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
    [User("qwert0p")]

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
                            "System",
                            "System.Linq",
                            "System.Diagnostics",
                            "System.Threading"
                        })
                        .WithEmitDebugInformation(true)
                );

                if (result != null)
                {
                    Send(result.ToString());
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
        [StartWith("!startuptimespam")]
        public async void UptimeSpam()
        {
            while (true) {
                var time = TimeSpan.FromMilliseconds(Environment.TickCount); 
                Send($"Сервак жив! Uptime: { time.Days } Days { time.Hours } Hours PogT");
                await Task.Delay(43200000);
            }
        }
        [StartWith("!startKILL")]
        public async void KILL()
        {
            while (true) {
                Send("-noted 2 商有任他的高直己。強新動片臺向離間的會怎狀！ 傷色人習等響講跑有。國是些行太中答。應油味動子這性票現，直備率黨不運說又好天現影怎願是到只且……不一多這答。慢聞？ 此國雨方光？子院岸人能、事羅陸要：寫一病度只地是變看樂們全事成們。 長一業價你語司簡有，因境如決層國不成，完性看這物國味紀；開出的得的不不坐東實適種之就點社級同底兒到，只是流卻而。 人離級基企背等了輕要得飛嗎方車方，說線光回指手安了同不中，親想因都。力香房病在路送存提立何林空生視度線用覺選位，報體有覺行。 不助地建級不也統手？ 經出過洋麼裡生。天心動後的，似招關國品，野的子現前勢活：爭文岸力的設怕在放業夠構上活對的病技轉因參面眼平運層平長，日設灣高經推子爭一教評我老相雙的價間關調北待我會了手！國來畫人委！品及是除金經張童是形，力大此提。我回。D̶̎̾I̶͐͐Ȇ̵̈ D̶̎̾I̶͐͐Ȇ̵̈");
                await Task.Delay(13000);
            }
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
