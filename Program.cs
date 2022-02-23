using System;
using System.Linq;
using Twitcher;
using Twitcher.Controllers;
using Twitcher.Controllers.JsonHelper;
using Twitcher.Controllers.APIHelper;
using System.Collections.Generic;
using LiphiBot2.Models;
using TwitchLib.Client.Events;
using System.Threading.Tasks;

namespace LiphiBot2
{
    public class Program
    {
        public static List<TokenInfo> Tokens { get; private set; }
        public static async Task Main(string[] args)
        {
            //FizzBuzz
            Tokens = GetTokens(args[0]);
            TokenInfo chatToken = Tokens.FirstOrDefault(x => x.TokenPurpose == "MAIN_CHAT");
            TokenInfo apiToken = Tokens.FirstOrDefault(x => x.TokenPurpose == "MAIN_API");
            var client = new TwitcherClient()
            .UseTwitchLibProvider(new TwitchLib.Client.Models.ConnectionCredentials(chatToken.UserName, chatToken.Token))
            .JoinChannels(new string[] {
                "LiphiTC",
                "Safrit22",
                //"33kk",
                //"RustKunXD",
                //"pajlada",
                //"Toni__Stark_",
                "ZakvielChannel"
            })
            .UseLogger(new ConsoleLoggerLiphi())
            .UseControllers()
            .UseJsonHelper(args[0])
            .UseAPIHelper(apiToken.ClientID, apiToken.Token)
            .BuildControllers()
            .Connect();
            // client.Bot.OnUserJoined += (object sender, OnUserJoinedArgs args) =>
            // {
            //     args.Username = args.Username.ToLower();
            //     switch (args.Username)
            //     {
            //         case "zakvielchannel":
            //         case "zakvielnight":
            //             client.Bot.SendMessage(args.Channel, "⚠ ВНИМАНИЕ! В ЧАТ ЗАШЁЛ ЗАК! ⚠");
            //             break;
            //         case "exx1dae":
            //         case "exx2dae":
            //             client.Bot.SendMessage(args.Channel, "⚠ ВНИМАНИЕ! В ЧАТ ЗАШЛА МАРИНА! ⚠");
            //             break;
            //         case "arrtur77":
            //             client.Bot.SendMessage(args.Channel, "⚠ ВНИМАНИЕ! В ЧАТ ЗАШЛЁЛ АРТУР! ⚠");
            //             break;
            //         case "nikover":
            //             client.Bot.SendMessage(args.Channel, "⚠ ВНИМАНИЕ! В ЧАТ ЗАШЛЁЛ КОВРИК! ⚠");
            //             break;
                    

            //     }
            // };
            while(true) {
                await Task.Delay(100000);
            }
        }
        private static List<TokenInfo> GetTokens(string path)
        {
            JsonHelper helper = new(path);
            return helper.GetObject<List<TokenInfo>>("TokenInfo", "Tokens");
        }
    }
}
