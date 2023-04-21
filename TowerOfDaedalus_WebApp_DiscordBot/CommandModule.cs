using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using log4net;

namespace HavocBot
{
    /// <summary>
    /// module class that stores all commands and phrases the bot should respond to
    /// </summary>
    public class CommandModule : ModuleBase<SocketCommandContext>
    {
        /// <summary>
        /// dummy command used only for testing
        /// TODO: remove prior to a release build
        /// </summary>
        /// <returns></returns>
        [Command("cookie")]
        [Summary("the bot eats a cookie")]
        public async Task cancelEventAsync()
        {
            await Context.Channel.SendMessageAsync("the bot eats a cookie");
        }
    }

}
