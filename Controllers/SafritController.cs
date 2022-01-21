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
using System.Threading;
using System.Threading.Tasks;
using Twitcher.Controllers.APIHelper;

namespace LiphiBot2.Controllers
{
    [Channel("Safrit22")]
    [Channel("33kk")]
    [User("any")]
    public class SafritController : Controller
    {

        private readonly JsonHelper _helper;
        private readonly APIHelper _api;
        private static Vote _cuurentVote;
        public SafritController(JsonHelper helper, APIHelper api)
        {
            _helper = helper;
            _api = api;
        }
        [StartWith("!когдастрим", IsFullWord = true)]
        [CoolDown(300)]
        public void WhenStream(string when)
        {
            var dateTime = _helper.GetObject<DateTime>("SafritStreams", "Streams");
            if (dateTime < DateTime.Now)
            {
                SendAnswer("Хз когда, стример ленивая жопа YEPTA");
                return;
            }
            SendAnswer("WoahBlanket 👉 " + dateTime.ToString("dd.MM.yyyy HH:mm"));

        }
        [User("Safrit22")]
        [User("LiphiTC")]
        [StartWith("!тогдастрим", IsFullWord = true)]
        public void ThenStream()
        {
            string dateNotParsed = Message.Message.Substring(11);

            if (string.IsNullOrEmpty(dateNotParsed))
            {
                SendAnswer("Ну и когда weirdChamp");
                return;
            }
            DateTime time;
            if (DateTimeRoutines.TryParseDateOrTime(dateNotParsed, DateTimeRoutines.DateTimeFormat.UK_DATE, out time))
            {
                _helper.EditObject<DateTime>("SafritStreams", "Streams", time.AddHours(-2));
                SendAnswer("записал PepoG");
                return;
            }
            SendAnswer("Ну и когда weirdChamp");
            return;
        }

        [StartWith("!туалет")]
        [CoolDown(30)]
        public void Tyalet()
        {
            SendAnswer("YEP 👉 https://i.nuuls.com/xaCQl.png");
        }

        [StartWith("!gdbrowser")]
        public void GDBrowser(User m)
        {
            if (m == null)
            {
                SendAnswer("https://gdbrowser.com WoahBlanket");
                return;
            }
            SendAnswer("https://gdbrowser.com WoahBlanket", m.UserName);
        }
        public static Dictionary<string, string> smileSpam { get; set; } = new();

        [CoolDown(100)]
        public void Spam()
        {
            if (smileSpam.Count == 0)
            {
                smileSpam = _helper.GetObject<Dictionary<string, string>>("SmilesSpamer", "SpamOn");
                if (smileSpam is null)
                    return;
            }
            var s = smileSpam.FirstOrDefault(x => x.Key == Message.Message);

            if (!s.Equals(default(KeyValuePair<string, string>)))
                Send(s.Value);


        }
        /*
        [StartWith("!асафритвчате", IsFullWord = true)]
        [CoolDown(20)]
        public async void YEP(User u)
        {
            u = u == null ? _api.User : u;
            var l = await _api.Channel.GetUsersAsync();
            bool isSafrit = l.Any(x => x.UserName == "safrit22");
            if (isSafrit)
            {
                SendAnswer("YEP", u.UserName);
            }
            else
            {
                SendAnswer("NOPE", u.UserName);

            }
        }
        */


        [StartWith("!level")]
        [CoolDown(30)]
        public void Level()
        {
            var level = _helper.GetObject<GDLevel>("SafritGdLevels", "CurrentLevel");
            if (level == null)
            {
                SendAnswer("Сейчас не играется ни какой левел WoahBlanket");
                return;
            }
            string prefix = level.Epic ? "Epic" : level.Featured ? "Featured" : level.Stars != 0 ? "Starred" : null;
            if (prefix != null)
            {
                SendAnswer($"[{prefix}] ({level.Difficulty}) {level.Name} by {level.Author} Song: {level.SongName} by {level.SongAuthor} ID: {level.Id} WoahBlanket");
                return;
            }
            SendAnswer($"({level.Difficulty}) {level.Name} by {level.Author} Song: {level.SongName} by {level.SongAuthor} ID: {level.Id} WoahBlanket");
        }
        [StartWith("!nextlevel")]
        [User("Safrit22")]
        [User("LiphiTC")]
        public void NextLevel()
        {
            var levels = _helper.GetObject<List<GDLevel>>("SafritGdLevels", "Queue");

            if (levels == null || levels.Count == 0)
            {
                SendAnswer("Нет уровней в очереде WoahBlanket");
                return;
            }
            var level = levels[0];
            levels.Remove(level);
            _helper.EditObject<List<GDLevel>>("SafritGdLevels", "Queue", levels);
            _helper.EditObject<GDLevel>("SafritGdLevels", "CurrentLevel", level);

            string prefix = level.Epic ? "Epic" : level.Featured ? "Featured" : level.Stars != 0 ? "Starred" : null;
            if (prefix != null)
            {
                SendAnswer($"[{prefix}] ({level.Difficulty}) {level.Name} by {level.Author} Song: {level.SongName} by {level.SongAuthor} ID: {level.Id} WoahBlanket");
                return;
            }
            SendAnswer($"({level.Difficulty}) {level.Name} by {level.Author} Song: {level.SongName} by {level.SongAuthor} ID: {level.Id} WoahBlanket");
        }
        [StartWith("!snap")]
        [User("Safrit22")]
        [User("LiphiTC")]
        public void KillChat()
        {
            SendAnswer("Ты чево наделал rooSnap");
        }


        [CoolDown(50)]
        [StartWith("!рандом")]
        public void Random(int? i, int? j)
        {
            if (i is null || j is null)
            {
                SendAnswer("ну и нахуй мне твои буквы WoahBlanket");
                return;
            }
            Random r = new Random();
            if (i > j)
            {
                SendAnswer("WoahBlanket 👉 " + r.Next(j.Value, i.Value));
                return;
            }
            SendAnswer("WoahBlanket 👉 " + r.Next(i.Value, j.Value));
            return;

        }
        [CoolDown(50)]
        [StartWith("!gillette")]
        public void Pizdec()
        {
            SendAnswer("ЭТО БЛЯТЬ ЧАТ САФРИТА, НЕ ДОЛБАЁБА, ИДИ ОТСЮДА НАХУЙ СО СВОЕЙ РЕКЛАМНОЙ ХУЙНЁЙ");
            return;

        }
        [CoolDown(50)]
        [StartWith("!этостоило100рублей")]
        public void Rubles()
        {
            SendAnswer("Starege 👉 https://clips.twitch.tv/SparklyFilthyPicklesCoolStoryBob-L0IdhquUH7CZ4jik");
            return;

        }
        [CoolDown(50)]
        [StartWith("!чатеры")]
        public async void Chatter()
        {
            var chatters = await _api.Channel.GetUsersAsync();
            SendAnswer("WoahBlanket 👉 " + chatters.Count);
        }
        [CoolDown(30)]
        [StartWith("!какполучитьвипкуузака", IsFullWord = true)]
        public void HowToVip(User u)
        {
            u ??= _api.User;
            SendAnswer("просто купи ЛОООООЛ 4HEader");
        }

        [CoolDown(30)]
        [StartWith("!voteban", IsFullWord = true)]
        public async void VoteBan(User u)
        {
            if (_cuurentVote != null)
            {
                SendAnswer("Уже идёт голосованеи на бан " + _cuurentVote.Target.UserName);
                return;
            }
            if (u == null)
            {
                SendAnswer("Укажите жертву MEGALUL");
                return;
            }
            CancellationTokenSource source = new();
            _cuurentVote = new Vote()
            {
                StartDate = DateTime.Now,
                Target = u,
                Creater = _api.User.UserName,
                RequiredVotes = (await _api.API.Undocumented.GetChattersAsync("safrit22")).Count / 3,
                VotedUser = new(),
                CancellationTokenSource = source
            };
            Send("Началось голосование на бан " + u.UserName + " MEGALUL");
            Task s = System.Threading.Tasks.Task.Delay(300000);
            await s;
            if (!s.IsCanceled)
            {
                _cuurentVote = null;
                Send("Прошло слишком много времени Sadge " + u.UserName + " не успели забанить NOPE");
            }

        }

        [StartWith("!banme ")]
        [User("x3_xto")]
        public void NOPE() {
            Send("/unban x3_xto");
            SendAnswer("NOPE");
        }

        [StartWith("!voteyep", IsFullWord = true)]
        public void VoteYEP(User u)
        {
            if (_cuurentVote == null)
            {
                SendAnswer("Сейчас не идёт голосование на бан NOPE");
                return;
            }
            if (_cuurentVote.VotedUser.Any(x => x == _api.User.UserID))
            {
                SendAnswer("Вы уже голосовали NOPE");
                return;
            }
            _cuurentVote.VotedUser.Add(_api.User.UserID);
            SendAnswer($"Вы успешно проголосовали PogT ({_cuurentVote.VotedUser.Count}/{_cuurentVote.RequiredVotes})");
            if (_cuurentVote.VotedUser.Count == _cuurentVote.RequiredVotes)
            {
                Send("Набралось необходимое количество голосов, баним " + _cuurentVote.Target.UserName + " PogT");
                Send("/timeout " + _cuurentVote.Target.UserName + " 10m Результат воутбана");
                _cuurentVote = null;
            }
        }

        public async void Balls()
        {
            string yep = Message.CustomRewardId;
            if (yep == "1b323ba3-6cdf-4309-9e6a-47042f848f2c")
            {
                string[] splted = Message.Message.Split(' ');
                foreach (string s in splted)
                {
                    long id;
                    if (long.TryParse(s, out id))
                    {
                        var client = new RestClient("https://gdbrowser.com/api");
                        var request = new RestRequest("level/" + id);
                        var response = await client.ExecuteGetAsync(request);
                        string idS = "";

                        GDLevel level = new();
                        if (response.Content == "-1")
                        {
                            idS = Message.Message;
                        }
                        else
                        {
                            level = JsonConvert.DeserializeObject<GDLevel>(response.Content);
                        }
                        level.Id ??= idS;
                        var levels = _helper.GetObject<List<GDLevel>>("SafritGdLevels", "Queue");
                        if (levels == null)
                            levels = new List<GDLevel>();

                        if (levels.Any(x => x.Id == level.Id))
                        {
                            SendAnswer("Этот уровень уже есть в очереди PETTHEPEPEGA Его позиция: " + levels.FindIndex(x => x.Id == level.Id));
                            return;
                        }

                        levels.Add(level);
                        _helper.EditObject<List<GDLevel>>("SafritGdLevels", "Queue", levels);
                        SendAnswer($"Уровень {level.Name} успешно добавлен в очередь PETTHEPEPEGA Его позиция: {levels.Count - 1}");
                        return;
                    }
                }
                SendAnswer("Укажите id левла");

            }
        }

    }

}
