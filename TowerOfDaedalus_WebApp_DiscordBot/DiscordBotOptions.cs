using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerOfDaedalus_WebApp_DiscordBot
{
    /// <summary>
    /// options class for the discord bot
    /// </summary>
    public class DiscordBotOptions
    {
        /// <summary>
        /// the authentication token for the bot
        /// </summary>
        public string botToken { get; set; }

        /// <summary>
        /// the ID of the server the bot should operate in
        /// </summary>
        public string targetServer { get; set; }

        /// <summary>
        /// the ID of the primary channel the bot should operate in
        /// </summary>
        public string targetPublicChannel { get; set; }

        /// <summary>
        /// the ID of the channel the bot should send messages in when the messages are meant for game masters
        /// </summary>
        public string targetGMChannel { get; set; }
    }
}
