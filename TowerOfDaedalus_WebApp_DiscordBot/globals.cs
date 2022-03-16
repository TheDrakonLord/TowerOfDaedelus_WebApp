using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace TowerOfDaedalus_WebApp_DiscordBot
{
    internal class globals
    {
        /// <summary>
        /// Collection of target channels for each guild
        /// </summary>
        public static Dictionary<string, ulong[]> guildSettings = new Dictionary<string, ulong[]>();

        /// <summary>
        /// Status message the bot displays on the server
        /// </summary>
        public static string statusMessage = "Hello There";

        /// <summary>
        /// A list of all guilds the bot has joined
        /// </summary>
        public static List<ulong> allGuilds = new List<ulong>();

        /// <summary>
        /// Holds the token for accessing discord
        /// </summary>
        public static string token = "";

        /// <summary>
        /// Logs a message to the console
        /// </summary>
        /// <param name="category">The category for the log message (ex. Type:)</param>
        /// <param name="Context">The context of the command called (only available in modules)</param>
        /// <param name="message">the message to be sent to the log</param>
        public static void logMessage(SocketCommandContext Context, string category, string message)
        {
            System.Diagnostics.Contracts.Contract.Requires(Context != null);
            Console.WriteLine($"{DateTime.Now.ToShortDateString(),-11}{System.DateTime.Now.ToLongTimeString(),-8} {category}: {message} by {Context.User.Username} in {Context.Guild.Name} ({Context.Guild.Id})");
        }

        /// <summary>
        /// Logs a message to the console
        /// </summary>
        /// <param name="category">The category for the log message (ex. Type:)</param>
        /// <param name="message">the message to be sent to the log</param>
        public static void logMessage(string category, string message)
        {
            Console.WriteLine($"{DateTime.Now.ToShortDateString(),-11}{System.DateTime.Now.ToLongTimeString(),-8} {category}: {message}");
        }

        /// <summary>
        /// Logs a message to the console
        /// </summary>
        /// <param name="message">the message to be sent to the log</param>
        public static void logMessage(string message)
        {
            Console.WriteLine($"{DateTime.Now.ToShortDateString(),-11}{System.DateTime.Now.ToLongTimeString(),-8} {message}");
        }
    }
}
