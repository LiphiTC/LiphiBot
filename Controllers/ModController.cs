using System;
using System.Linq;
using Twitcher.Controllers;
using Twitcher.Controllers.JsonHelper;
using Twitcher.Controllers.APIHelper;
using Twitcher.Controllers.Attributes;
using LiphiBot2.Models;
using System.Collections.Generic;

namespace MuteBot
{
    [Channel("safrit22")]
    [Channel("RustKunXD")]
    [User("LiphiTC")]
    [User("NeXySssss")]
    [User("Safrit22")]
    [User("akira_kitamura_qq")]
    [User("l_abrael_i")]
    [User("un1cornpr0")]
    [User("woahblanketbot")]
    public class ModController : Controller
    {
        private readonly APIHelper _api;
        private readonly JsonHelper _json;
        public ModController(APIHelper api, JsonHelper json)
        {
            _api = api;
            _json = json;
        }
        [StartWith("!addrule", IsFullWord = true)]
        public void AddRule()
        {
            string[] splited = Message.Message.Split(' ');
            if (splited.Length < 4)
            {
                SendAnswer("<номер> <время мута> <правило> PETTHEPEPEGA");
                return;
            }
            var rules = _json.GetObject<List<Rule>>("Rules", "Rules");
            if (rules == null)
                rules = new List<Rule>();

            if (rules.Any(x => x.ID == splited[1]))
            {
                SendAnswer("Это правило уже есть PETTHEPEPEGA");
                return;
            }
            var splitedList = splited.ToList();
            splitedList.RemoveAt(0);
            splitedList.RemoveAt(0);
            splitedList.RemoveAt(0);

            rules.Add(new Rule() { ID = splited[1], MuteTime = splited[2], RuleText = string.Join(' ', splitedList) });
            _json.EditObject<List<Rule>>("Rules", "Rules", rules);
            SendAnswer("Правило успешно добавлено Starege");
        }
        [StartWith("!мут", IsFullWord = true)]
        public void Mute(User user)
        {
            string[] splited = Message.Message.Split(' ');
            if (splited.Length < 3)
            {
                SendAnswer("<Пользователь> <Приавило> WoahBlanket");
                return;
            }
            string ruleString = splited[2];
            if (user == null)
            {
                SendAnswer("Нет такого юзера Starege");
                return;
            }
            if (ruleString == null)
            {
                SendAnswer("Укажите правило");
                return;
            }
            var rules = _json.GetObject<List<Rule>>("Rules", "Rules");
            if (rules == null)
                rules = new List<Rule>();

            var rule = rules.FirstOrDefault(x => x.ID == ruleString);

            if (rule == null)
            {
                SendAnswer("Неизвестное правило");
                return;
            }
            Send($"/timeout {user.UserName} {rule.MuteTime}{rule.RuleText}");
            SendAnswer($"Вы были замученны на {rule.MuteTime} за {rule.ID}: {rule.RuleText}", user.UserName);
        }
        [StartWith("!changeid", IsFullWord = true)]
        public void ChangeId()
        {
            string[] splited = Message.Message.Split(' ');
            if (splited.Length < 3)
            {
                SendAnswer("<Старое id> <Новое id> WoahBlanket");
                return;
            }

            var rules = _json.GetObject<List<Rule>>("Rules", "Rules");
            if (rules == null)
                rules = new List<Rule>();

            var rule = rules.FirstOrDefault(x => x.ID == splited[1]);

            if (rule == null)
            {
                SendAnswer("Неизвестное правило");
                return;
            }
            if (rules.Any(x => x.ID == splited[2]))
            {
                SendAnswer("Это id уже используется");
                return;
            }
            rule.ID = splited[2];
            _json.EditObject<List<Rule>>("Rules", "Rules", rules);
            SendAnswer($"Успешно заменено {splited[1]} > {splited[2]} PETTHEBONK ");

        }
        [StartWith("!changetime", IsFullWord = true)]
        public void ChangeTime()
        {
            string[] splited = Message.Message.Split(' ');
            if (splited.Length < 3)
            {
                SendAnswer("<id> <Новое время мута> PETTHEPEEPOGA ");
                return;
            }

            var rules = _json.GetObject<List<Rule>>("Rules", "Rules");
            if (rules == null)
                rules = new List<Rule>();

            var rule = rules.FirstOrDefault(x => x.ID == splited[1]);

            if (rule == null)
            {
                SendAnswer("Неизвестное правило");
                return;
            }
            rule.MuteTime = splited[2];
            _json.EditObject<List<Rule>>("Rules", "Rules", rules);
            SendAnswer($"Успешно заменено время мута для правила {splited[1]}");

        }

        [StartWith("!changefullinfo", IsFullWord = true)]
        public void ChangeFullText()
        {
            string[] splited = Message.Message.Split(' ');
            if (splited.Length < 3)
            {
                SendAnswer("<id> <Новый полный текст> PETTHEPEEPOGA ");
                return;
            }

            var rules = _json.GetObject<List<Rule>>("Rules", "Rules");
            if (rules == null)
                rules = new List<Rule>();

            var rule = rules.FirstOrDefault(x => x.ID == splited[1]);

            if (rule == null)
            {
                SendAnswer("Неизвестное правило");
                return;
            }
            List<string> splitedList = splited.ToList();

            splitedList.RemoveAt(0);
            splitedList.RemoveAt(1);
            
            rule.FullRuleText = string.Join(' ', splitedList);

            _json.EditObject<List<Rule>>("Rules", "Rules", rules);
            SendAnswer($"Успешно заменён полный текст для: {splited[1]}");

        }


    }
}