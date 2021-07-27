using System;
using Twitcher;
using Twitcher.Controllers;
using Twitcher.Controllers.JsonHelper;
using Twitcher.Controllers.APIHelper;

namespace LiphiBot2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var client = new TwitcherClient()
            .UseTwitchLibProvider(new TwitchLib.Client.Models.ConnectionCredentials("LiphiTC", ""))   
            .JoinChannels(new string[] {
                "zakvielchannel",
                "LiphiTC",
                "Safrit22"
            })
            .UseControllers()
            .UseJsonHelper("JsonSave")
            .UseAPIHelper("gp762nuuoqcoxypju8c569th9wz7q5", "3vy90gyb0qnunh0ldmfok9428fpvi7")
            .BuildControllers()
            .Connect();

            Console.ReadLine();
        }
    }
}
