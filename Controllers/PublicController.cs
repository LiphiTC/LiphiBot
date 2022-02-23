using System;
using System.Collections.Generic;
using Twitcher;
using System.Linq;
using Twitcher.Controllers;
using Twitcher.Controllers.Attributes;
using Twitcher.Controllers.JsonHelper;
using Twitcher.Controllers.APIHelper;
using System.Diagnostics;

namespace LiphiBot2.Controllers
{
    [Channel("Safrit22")]
    [Channel("LiphiTC")]
    [Channel("ZakvielChannel")]
    [Channel("33kk")]
    [User("any")]
    public class PublicController : Controller
    {
        private readonly JsonHelper _helper;
        private readonly APIHelper _api;
        public PublicController(JsonHelper helper, APIHelper api)
        {
            _helper = helper;
            _api = api;
        }

        [StartWith("!addeblan", IsFullWord = true)]
        [CoolDown(40)]
        public void AddEblan(User u1)
        {
            if (u1 == null)
            {
                SendAnswer("Укажите ник EBLAN a");
                return;
            }
            var eblans = _helper.GetObject<List<string>>("Ebalns", "Ebalns");
            if (eblans == null)
                eblans = new();

            if (eblans.Any(x => x == u1.UserName))
            {
                SendAnswer("Этот EBLAN уже есть в базе EBLAN");
                return;
            };
            eblans.Add(u1.UserName);
            _helper.EditObject<List<string>>("Ebalns", "Ebalns", eblans);
            SendAnswer("EBLAN успешно добавлен EBLAN");

        }
        [StartWith("!eblans", IsFullWord = true)]
        [CoolDown(40)]
        public async void Eblans(User u)
        {
            if ((await _api.API.V5.Streams.GetStreamByUserAsync(_api.Channel.Broadcaster.UserID)).Stream != null)
                return;

            u = u == null ? _api.User : u;
            var eblans = _helper.GetObject<List<string>>("Ebalns", "Ebalns");
            string result = "EBLAN 👉 ";
            for (int i = 0; i < eblans.Count; i++)
            {
                if (i == eblans.Count - 1)
                {
                    result += eblans[i];
                    continue;
                }
                result += (eblans[i] + ", ");
            }
            SendAnswer(result, u.UserName);
        }
        [StartWith("!logs", IsFullWord = true)]
        public void Logs(User u)
        {

            if (u == null)
            {
                SendAnswer("YEP 👉 https://justlog.kkx.one");
                return;
            }
            SendAnswer($"YEP 👉 https://justlog.kkx.one/?channel={_api.Channel.Broadcaster.UserName}&username={u.UserName}");
        }

        [StartWith("&ping", IsFullWord = true)]
        public void Ping(User user)
        {
            user ??= _api.User;
            ProcessStartInfo startInfo = new ProcessStartInfo() { FileName = "/bin/bash", Arguments = "/bin/sensors", };
            Process proc = new Process() { StartInfo = startInfo, };
            proc.Start();
            proc.OutputDataReceived += (object sender, DataReceivedEventArgs args) =>
            {
                Console.WriteLine(args.Data);
            };

        }


    }
}
