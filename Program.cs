using System;
using System.Linq;
using Twitcher;
using Twitcher.Controllers;
using Twitcher.Controllers.JsonHelper;
using Twitcher.Controllers.APIHelper;
using System.Collections.Generic;
using LiphiBot2.Models;

namespace LiphiBot2
{
    public class Program
    {
        public static List<TokenInfo> Tokens { get; private set; }
        public static void Main(string[] args)
        {
            Tokens = GetTokens(args[0]);
            TokenInfo chatToken = Tokens.FirstOrDefault(x => x.TokenPurpose == "MAIN_CHAT");
            TokenInfo apiToken = Tokens.FirstOrDefault(x => x.TokenPurpose == "MAIN_API");
            var client = new TwitcherClient()
            .UseTwitchLibProvider(new TwitchLib.Client.Models.ConnectionCredentials(chatToken.UserName, chatToken.Token))
            .JoinChannel("ZakvielChannel")
            .JoinChannels(new string[] {
                "LiphiTC",
                "Safrit22",
                "33kk",
                "WoahBlanketBot"
            })
            .UseLogger(new ConsoleLoggerLiphi())
            .UseControllers()
            .UseJsonHelper(args[0])
            .UseAPIHelper(apiToken.ClientID, apiToken.Token)
            .BuildControllers()
            .Connect();

            Console.ReadLine();
        }
        private static List<TokenInfo> GetTokens(string path)
        {
            JsonHelper helper = new(path);
            return helper.GetObject<List<TokenInfo>>("TokenInfo", "Tokens");
        }
    }
}
