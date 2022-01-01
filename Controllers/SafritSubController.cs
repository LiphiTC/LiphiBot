using System;
using System.Collections.Generic;
using Twitcher;
using System.Linq;
using Twitcher.Controllers;
using Twitcher.Controllers.Attributes;
using Twitcher.Controllers.JsonHelper;
using LiphiBot2.Models;

namespace LiphiBot2.Controllers
{
    [Channel("any")]
    [User("any")]
    public class SafritSubController : Controller
    {
        private readonly JsonHelper _helper;
        public SafritSubController(JsonHelper helper)
        {
            _helper = helper;
        }


        [StartWith("!addsub", IsFullWord = true)]
        [User("liphitc")]
        [User("Safrit22")]
        public void AddSub()
        {
            string[] splited = Message.Message.Split(' ');
            if (splited.Length < 3)
            {
                SendAnswer("weirdChamp");
                return;
            }
            float subCount;
            if (float.TryParse(splited[1], out subCount))
            {
                var subs = _helper.GetObject<List<Sub>>("Subs", "Subs");
                if (subs == null)
                    subs = new List<Sub>();

                var userSub = subs.FirstOrDefault(x => x.UserName == splited[2].ToLower());

                if (userSub == null)
                    userSub = new Sub() { UserName = splited[2].ToLower() };

                userSub.SubCount += subCount;
                subs.Remove(userSub);
                subs.Add(userSub);
                _helper.EditObject<List<Sub>>("Subs", "Subs", subs);
                SendAnswer("баланс _" + splited[2] + " успешно пополнен peepoClap  Его текущий баланс: " + userSub.SubCount + " " + GetSubName(subCount) + " pOg");
                return;
            }
            SendAnswer("weirdChamp 👉 {Кол-во} {Кому}");

        }
        
        [StartWith("!removesub", IsFullWord = true)]
        [User("liphitc")]
        [User("Safrit22")]
        public void RemoveSub()
        {
            string[] splited = Message.Message.Split(' ');
            if (splited.Length < 2)
            {
                SendAnswer("weirdChamp");
                return;
            }
            float subCount;
            if (float.TryParse(splited[1], out subCount))
            {
                var subs = _helper.GetObject<List<Sub>>("Subs", "Subs");
                if (subs == null)
                    subs = new List<Sub>();

                var userSub = subs.FirstOrDefault(x => x.UserName == splited[2].ToLower());

                if (userSub == null)
                {
                    SendAnswer("Этот юзер не зареган в бд PETTHEPEPEGA");
                    return;
                }

                if (userSub.SubCount < subCount)
                {
                    SendAnswer("У этого юзера нет столько сабок PETTHEPEPEGA");
                    return;
                }
                userSub.SubCount -= subCount;
                subs.Remove(userSub);
                subs.Add(userSub);
                _helper.EditObject<List<Sub>>("Subs", "Subs", subs);
                SendAnswer("c баланса _" + splited[2] + " успешно снято " + subCount + " " + GetSubName(subCount) + " WoahSadBlanket Его текущий баланс: " + userSub.SubCount + " сабок Sadge");
                return;
            }
            SendAnswer("weirdChamp 👉 {Кол-во} {Кому}");
        }

        [StartWith("!giftsub", IsFullWord = true)]
        public void GiftSub()
        {
            string[] splited = Message.Message.Split(' ');
            if (splited.Length < 2)
            {
                SendAnswer("weirdChamp");
                return;
            }
            float subCount;
            if (float.TryParse(splited[1], out subCount))
            {
                var subs = _helper.GetObject<List<Sub>>("Subs", "Subs");
                if (subs == null)
                    subs = new List<Sub>();

                var userSub = subs.FirstOrDefault(x => x.UserName == Message.Username);

                if (userSub == null)
                {
                    SendAnswer("Ты не зареган в бд PETTHEPEPEGA");
                    return;
                }

                if (userSub.SubCount < subCount)
                {
                    SendAnswer("У тебя нет столько сабок PETTHEPEPEGA");
                    return;
                }
                var userSubTo = subs.FirstOrDefault(x => x.UserName == splited[2].ToLower());
                if (userSubTo == null)
                {
                    userSubTo = new Sub() { UserName = splited[2].ToLower() };
                }
                userSub.SubCount -= subCount;
                userSubTo.SubCount += subCount;
                subs.Remove(userSub);
                subs.Add(userSub);
                subs.Remove(userSubTo);
                subs.Add(userSubTo);
                _helper.EditObject<List<Sub>>("Subs", "Subs", subs);
                SendAnswer("вы успешно подарили " + subCount + " " + GetSubName(subCount) + " _" + splited[2] + " WoahBlanket");
                return;
            }
            SendAnswer("weirdChamp 👉 {Кол-во} {Кому}");
        }

        [StartWith("!howmuchsubsdoihave")]
        public void HowMuchSubA()
        {
            var subs = _helper.GetObject<List<Sub>>("Subs", "Subs");
            var user = subs.FirstOrDefault(x => x.UserName == Message.Username);
            if (user == null || user.SubCount == 0)
            {
                SendAnswer("0 YEPTA");
                return;
            }
            if (user.SubCount < 1)
            {
                SendAnswer(user.SubCount + " WoahBlanket");
                return;
            }
            SendAnswer(user.SubCount + " pOg");
                return;
        }

        
        [NonCommand]
        private string GetSubName(float count)
        {
            if (count == 1)
                return "сабка";

            return "сабки";

        }

    }
}
