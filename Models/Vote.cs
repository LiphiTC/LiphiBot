using System;
using Twitcher.Controllers.APIHelper;
using System.Collections.Generic;
using System.Threading;

namespace LiphiBot2.Models
{
    public class Vote
    {
        public DateTime StartDate { get; set; }
        public User Target { get; set; }
        public string Creater { get; set; }
        public int RequiredVotes { get; set; }
        public List<string> VotedUser { get; set; }
        public CancellationTokenSource CancellationTokenSource { get; set; }
    }
}