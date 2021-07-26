using System;
using System.Collections.Generic;
using Twitcher;
using System.Linq;
using Twitcher.Controllers;
using Twitcher.Controllers.Attributes;
using Twitcher.Controllers.JsonHelper;


namespace LiphiBot2.Controllers
{
    [Channel("any")]
    [User("any")]
    public class PublicController : Controller
    {
        public static Dictionary<string, string> smileSpam { get; set; } = new();
        private readonly JsonHelper _helper;
        public PublicController(JsonHelper helper)
        {
            _helper = helper;
        }
        [CoolDown(100)]
        public void Spam()
        {
            if (smileSpam.Count == 0)
            {
                smileSpam = _helper.GetObject<Dictionary<string, string>>("SmilesSpamer", "SpamOn");
                if(smileSpam is null)
                    return;
            }
            var s = smileSpam.FirstOrDefault(x => x.Key == Message.Message);

            if(!s.Equals(default(KeyValuePair<string, string>)))
                Send(s.Value);


        }

    }
}
