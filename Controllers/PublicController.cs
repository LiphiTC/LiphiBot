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
        private readonly JsonHelper _helper;
        public PublicController(JsonHelper helper)
        {
            _helper = helper;
        }
        
    }
}
