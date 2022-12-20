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
    public class CommandModule : ModuleBase<SocketCommandContext>
    {
        [Command("cookie")]
        [Summary("the bot eats a cookie")]
        public async Task cancelEventAsync()
        {
            await Context.Channel.SendMessageAsync("the bot eats a cookie");
        }
    }

}
