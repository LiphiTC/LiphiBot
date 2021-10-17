using System;
using System.Linq;
using Twitcher.Controllers;
using Twitcher.Controllers.APIHelper;
using Twitcher.Controllers.Attributes;

namespace MuteBot
{
    [User("Any")]
    [Channel("LiphiTC")]
    public class MuteController : Controller
    {
        private readonly APIHelper _api;
        public MuteController(APIHelper api)
        {
            _api = api;
        }
        [StartWith("!ойчто", IsFullWord = true)]
        public void SecondMute() => Send($"/timeout {Message.Username} 1s ой что YEP ?");
    

        [StartWith("!мутмне", IsFullWord = true)]
        public void FiveMinuteMute()
        {
            Send($"/timeout {Message.Username} 5m сам просил");
        }

        [StartWith("!нупиздапизда", IsFullWord = true)]
        public void NupizdaPizda()
        {
            Send($"/timeout {Message.Username} 30m ну пизда так пизда");
        }
        [StartWith("!kill", IsFullWord = true)]
        public void Kill()
        {
            Send($"/delete {Message.Id}");

            
        }
        [StartWith("!гн", IsFullWord = true)]
        public void Gn()
        {
            Send($"/timeout {Message.Username} 40m спокойной");
        }
        [StartWith("!snap", IsFullWord = true)]
        [User("Safrit22")]
        [User("LiphiTC")]
        public async void KillChat(int? percentage)
        {
            var chaters = await _api.Channel.GetUsersAsync();
            chaters = chaters.OrderBy(x => Guid.NewGuid()).ToList();
            SendAnswer("Щёлк YEPTA");
            if (percentage == null)
            {
                for (int i = 0; i < chaters.Count / 2; i++)
                {
                    Send("/timeout " + chaters[i].UserName + " 3m Щёлк");
                }
                return;
            }
            for (int i = 0; i < (int)((float)chaters.Count / 100f * percentage.Value); i++)
            {
                Send("/timeout " + chaters[i].UserName + " 3m Щёлк");
            }
            return;

        }
        [StartWith("!snapcount")]
        [User("Safrit22")]
        [User("LiphiTC")]
        public async void KillChatAmmout(int? count)
        {
            var chaters = await _api.Channel.GetUsersAsync();
            chaters = chaters.OrderBy(x => Guid.NewGuid()).ToList();
            SendAnswer("Щёлк YEPTA");
            if (count == null)
            {
                for (int i = 0; i < chaters.Count / 2; i++)
                {
                    Send("/timeout " + chaters[i].UserName + " 3m Щёлк");
                }
                return;
            }
            for (int i = 0; i < count; i++)
            {
                if(chaters.Count < i)
                    continue;

                Send("/timeout " + chaters[i].UserName + " 3m Щёлк");
            }
            return;

        }


    }
}