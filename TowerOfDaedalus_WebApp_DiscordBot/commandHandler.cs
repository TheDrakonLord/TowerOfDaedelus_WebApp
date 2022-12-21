using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord.Commands;
using System;

namespace TowerOfDaedalus_WebApp_DiscordBot
{
    internal class commandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly ILogger<DiscordBot> _logger;

        /// <summary>
        /// Retrieve client and CommandService instance via actor
        /// </summary>
        /// <param name="client">the discord client</param>
        /// <param name="commands">the command service</param>
        public commandHandler(DiscordSocketClient client, CommandService commands, ILogger<DiscordBot> logger)
        {
            _commands = commands;
            _client = client;
            _logger = logger;
        }

        /// <summary>
        /// Identifies and loads the known commands
        /// </summary>
        /// <returns>returns task complete</returns>
        public async Task installCommandsAsync()
        {
            // Hook the MessageReceived event into our command handler
            _client.MessageReceived += handleCommandAsync;

            // Here we discover all of the command modules in the entry 
            // assembly and load them. Starting from Discord.NET 2.0, a
            // service provider is required to be passed into the
            // module registration method to inject the 
            // required dependencies.
            //
            // If you do not use Dependency Injection, pass null.
            // See Dependency Injection guide for more information.
            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(),
                                            services: null).ConfigureAwait(false);
        }

        /// <summary>
        /// Recieves a command and interprets it
        /// </summary>
        /// <param name="messageParam">message recieved form the server</param>
        /// <returns>returns task complete</returns>
        private async Task handleCommandAsync(SocketMessage messageParam)
        {
            // Don't process the command if it was a system message
            if (messageParam as SocketUserMessage == null) return;

            // Create a number to track where the prefix ends and the command begins
            int argPos = 0;

            // Determine if the message is a command based on the prefix and make sure no bots trigger commands
            if (!((messageParam as SocketUserMessage).HasCharPrefix('!', ref argPos) ||
                (messageParam as SocketUserMessage).HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
(messageParam as SocketUserMessage).Author.IsBot)
                return;

            // Create a WebSocket-based command context based on the message
            var context = new SocketCommandContext(_client, messageParam as SocketUserMessage);

            // Execute the command with the command context we just
            // created, along with the service provider for precondition checks.
            await _commands.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: null).ConfigureAwait(false);

            //log the command recieve in the log
            _logger.LogInformation($"{DateTime.Now.ToShortDateString(),-11}{System.DateTime.Now.ToLongTimeString(),-8} Command: {messageParam as SocketUserMessage}");
            
        }
    }
}
