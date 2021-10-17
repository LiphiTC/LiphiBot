using System;

namespace LiphiBot2.Models
{
    public class MutedUser
    {
        public string UserID { get; set; }
        public string Moderator { get; set; }
        public string Rule { get; set; }
        public string MuteGroup { get; set; }
        public DateTime MutedOn { get; set; }

    }
}