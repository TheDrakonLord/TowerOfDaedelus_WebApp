using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerOfDaedalus_WebApp_DiscordBot
{
    public class DiscordBotOptions
    {
        public string botToken { get; set; }
        public string targetServer { get; set; }
        public string targetPublicChannel { get; set; }
        public string targetGMChannel { get; set; }
    }
}
