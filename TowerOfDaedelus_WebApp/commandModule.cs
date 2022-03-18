using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TowerOfDaedalus_WebApp_DiscordBot
{
    /// <summary>
    /// Set of commands only executable by Admins or the Bot Owner
    /// </summary>
    public class commandModule : ModuleBase<SocketCommandContext>
    {

        [Command("cookie")]
        [Summary("this is a test command")]
        public async Task testComand()
        {
            await Context.Channel.SendMessageAsync("the bot eats a cookie");
        }
    }
}
