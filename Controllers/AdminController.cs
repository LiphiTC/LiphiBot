using System;
using System.Collections.Generic;
using Twitcher;
using System.Linq;
using Twitcher.Controllers;
using Twitcher.Controllers.Attributes;
using Twitcher.Controllers.JsonHelper;
using System.Threading;
using System.Threading.Tasks;

namespace LiphiBot2.Controllers
{
    [Channel("any")]
    [User("LiphiTC")]
    public class AdminController : Controller
    {
        private bool _isSpam = false;
        private readonly JsonHelper _helper;
        public AdminController(JsonHelper helper)
        {
            _helper = helper;
        }
        [Same("WoahBlanket s")]
        public async void Spam()
        {
            _isSpam = true;

            while (_isSpam)
            {
                Random rand = new Random();
                await Task.Delay(rand.Next(540000, 660000));
                //await Task.Delay(1000);
                Send("WoahSadBlanket");
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
            PublicController.smileSpam.Add(s1 + ' ' + s2, s1 + ' ' + s2);
            _helper.EditObject<Dictionary<string, string>>("SmilesSpamer", "SpamOn", s);
            SendAnswer("готово WoahBlanket");
        }
    }
}
