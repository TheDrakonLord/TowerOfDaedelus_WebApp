using Discord;
using Discord.Net;
using Discord.WebSocket;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;

namespace TowerOfDaedalus_WebApp_DiscordBot
{
    public static class CommandModule
    {
        public static async void CreateCommands(SocketGuild guild, ILogger<DiscordBot> logger) 
        {
            var cookieCommand = new SlashCommandBuilder();
            cookieCommand.WithName("cookie");
            cookieCommand.WithDescription("Test command");


            try
            {
                await guild.CreateApplicationCommandAsync(cookieCommand.Build());
            }
            catch (ApplicationCommandException exception)
            {
                var json = JsonConvert.SerializeObject(exception.Errors, Formatting.Indented);
                logger.LogCritical(exception, json);
            }

        }

        public static async Task SlashCommandHandler(SocketSlashCommand command)
        {
            switch (command.CommandName)
            {
                case "cookie":
                    await cookieCommand(command);
                    break;
                default:
                    break;
            }
        }

        public static async Task cookieCommand(SocketSlashCommand command)
        {
            await command.RespondAsync("The bot eats a cookie");
        }
    }
}
