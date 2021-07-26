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
    public class SafritController : Controller
    {
        private readonly JsonHelper _helper;
        private readonly APIHelper _api;
        public SafritController(JsonHelper helper, APIHelper api)
        {
            _helper = helper;
            _api = api;
        }
        [StartWith("!когдастрим", IsFullWord = true)]
        [CoolDown(300)]
        public void WhenStream()
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
                _helper.EditObject<DateTime>("SafritStreams", "Streams", time);
                SendAnswer("записал PepoG");
                return;
            }
            SendAnswer("Ну и когда weirdChamp");
            return;
        }
        [StartWith("!правила")]
        public void Rules() => SendAnswer("Вот правила YEPTA 👉 https://i.nuuls.com/eaSy4.png https://i.nuuls.com/WlfLs.png");

        [StartWith("!туалет")]
        [CoolDown(30)]
        public void Tyalet() => SendAnswer("YEP 👉 https://i.nuuls.com/xaCQl.png");

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

        [StartWith("!bttv")]
        [CoolDown(20)]
        public void BTTV(User m)
        {
            if (m == null)
            {
                SendAnswer("https://betterttv.com WoahBlanket");
                return;
            }
            SendAnswer("https://betterttv.com WoahBlanket", m.UserName);
        }
        [StartWith("!7tv")]
        [CoolDown(20)]
        public void Seventv(User m)
        {
            if (m == null)
            {
                SendAnswer("https://7tv.app WoahBlanket");
                return;
            }
            SendAnswer("https://7tv.app WoahBlanket", m.UserName);
        }
        [StartWith("!ffz")]
        [CoolDown(20)]
        public void FFZ(User m)
        {
            if (m == null)
            {
                SendAnswer("https://www.frankerfacez.com WoahBlanket");
                return;
            }
            SendAnswer("https://www.frankerfacez.com WoahBlanket", m.UserName);
        }

        [StartWith("!addlevel")]
        [CoolDown(120)]
        public async void AddLevel(int? id)
        {
            if (id == null)
            {
                SendAnswer("Введите id левла PETTHEPEPEGA");
                return;
            }
            var client = new RestClient("https://gdbrowser.com/api");
            var request = new RestRequest("level/" + id);
            var response = await client.ExecuteGetAsync(request);
            if (response.Content == "-1")
            {
                SendAnswer("неизвестный левел PETTHEPEPEGA");
                return;
            }
            var level = JsonConvert.DeserializeObject<GDLevel>(response.Content);

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
        }
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

        [CoolDown(20)]
        [StartWith("!follow")]
        public async void Follow(User u1, User u2)
        {
            DateTime startDate;
            if (u1 is null && u2 is null)
                startDate = await _api.User.GetFollowStartDateAsync(_api.Channel);
            else if (u2 is null)
                startDate = await u1.GetFollowStartDateAsync(_api.Channel);
            else if (u1 is null)
                startDate = await _api.User.GetFollowStartDateAsync(new Channel(_api.API, u2));
            else
                startDate = await u1.GetFollowStartDateAsync(new Channel(_api.API, u2));


            SendAnswer(startDate.ToString());
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
        [StartWith("!чатеры")]
        public async void Chatter()
        {
            var chatters = await _api.Channel.GetUsersAsync();
            SendAnswer("WoahBlanket 👉 " + chatters.Count);
        }

    }

}
