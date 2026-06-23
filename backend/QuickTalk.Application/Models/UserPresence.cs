using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTalk.Application.Models
{
    public class UserPresence
    {
        public HashSet<string> Connections { get; set; } = new();

        //Last time the user did something
        public DateTime LastActivity { get; set; } = DateTime.UtcNow;

        //Last time user went offline
        public DateTime? LastSeen { get; set; }
    }
}
