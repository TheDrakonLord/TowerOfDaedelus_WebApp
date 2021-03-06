using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord;
using Discord.Commands;
using System.Xml.Linq;
using System.IO;

namespace TowerOfDaedalus_WebApp_DiscordBot
{
    public class DiscordBot_BackgroundWorker : BackgroundService
    {
        /// <summary>
        /// Status message the bot displays on the server
        /// </summary>
        private static string statusMessage = "Hello There";

        /// <summary>
        /// A list of all guilds the bot has joined
        /// </summary>
        private static List<ulong> allGuilds = new List<ulong>();

        private readonly ILogger<DiscordBot_BackgroundWorker> _logger;

        //declare necessary variables
        private static DiscordSocketClient _client;
        private commandHandler _cHandler;
        private CommandService _cService;
        private static IMessageChannel _mainChannel;
        private static IMessageChannel _utilityChannel;

        public DiscordBot_BackgroundWorker(ILogger<DiscordBot_BackgroundWorker> logger, IOptions<DiscordBotOptions> optionsAccessor)
        {
            _logger = logger;
            Options = optionsAccessor.Value;
        }



        public DiscordBotOptions Options { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await StartBot(stoppingToken);
            }
        }

        private async Task StartBot(CancellationToken stoppingToken)
        {
            //initialize the client, command handler, and command service
            _client = new DiscordSocketClient();
            _cService = new CommandService();
            _cHandler = new commandHandler(_client, _cService, _logger);




            _client.Log += Log;


            // have the client login and start
            await _client.LoginAsync(TokenType.Bot, Options.botToken).ConfigureAwait(false);
            await _client.StartAsync().ConfigureAwait(false);
            await _client.SetGameAsync(statusMessage, null, ActivityType.Playing).ConfigureAwait(false);
            _client.JoinedGuild += joinedGuild;
            _client.LeftGuild += leftGuild;
            _client.Ready += botReady;


            // load the commands
            await _cHandler.installCommandsAsync().ConfigureAwait(false);
            // Block this task until the program is closed.
            await Task.Delay(-1, stoppingToken).ConfigureAwait(false);
        }

        /// <summary>
        /// When a message is recieved this sends the appropriate message to the log
        /// </summary>
        /// <param name="msg">Message to be sent to the log</param>
        /// <returns>completed task state</returns>
        private Task Log(LogMessage msg)
        {
            // queue the message to be sent to the log
            _logger.LogInformation(msg.ToString());
            return Task.CompletedTask;
        }

        /// <summary>
        /// Event handler for when the bot joins a guild
        /// </summary>
        /// <param name="newGuild">the guid the bot just joined</param>
        /// <returns>task complete</returns>
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        private async Task joinedGuild(SocketGuild newGuild)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            if (!allGuilds.Contains(newGuild.Id))
            {
                allGuilds.Add(newGuild.Id);
                _logger.LogInformation($"{DateTime.Now.ToShortDateString(),-11}{System.DateTime.Now.ToLongTimeString(),-8} Connected to Guild: {newGuild.Name} ({newGuild.Id})");

                //globals.commandStorage.Element("guilds").Add(new XElement("guild", newGuild.Id.ToString()));
                //globals.commandStorage.Save(globals.storageFilePath);
            }
            else
            {
                _logger.LogWarning($"Join error. Already connected to {newGuild.Name} ({newGuild.Id})");
            }
        }

        /// <summary>
        /// Event handler for when the bot leaves a guild
        /// </summary>
        /// <param name="leavingGuild">the guild the bot left</param>
        /// <returns>task complete</returns>
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        private async Task leftGuild(SocketGuild leavingGuild)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            if (allGuilds.Contains(leavingGuild.Id))
            {
                allGuilds.Remove(leavingGuild.Id);
                _logger.LogInformation($"The bot has left the guild {leavingGuild.Name} ({leavingGuild.Id})");
            }
            else
            {
                _logger.LogWarning($"Bot departure error. {leavingGuild.Name} ({leavingGuild.Id}) not found in collection");
            }
        }

        /// <summary>
        /// Changes the bot's status message
        /// </summary>
        /// <param name="message">the new status message to be set</param>
        /// <returns></returns>
        public static void statusChange(string message)
        {
            _client.SetGameAsync(message, null, ActivityType.Playing).ConfigureAwait(false);
        }

        /// <summary>
        /// Event handler for when the bot has finished loading and is ready to run
        /// </summary>
        /// <returns></returns>
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        private async Task botReady()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            _logger.LogInformation(Strings.logBotReady);
            IReadOnlyCollection<SocketGuild> conGuilds = _client.Guilds;
            foreach (SocketGuild x in conGuilds)
            {
                if (!allGuilds.Contains(x.Id))
                {
                    allGuilds.Add(x.Id);
                    _logger.LogInformation($"{DateTime.Now.ToShortDateString(),-11}{System.DateTime.Now.ToLongTimeString(),-8} Connected to Guild: {x.Name} ({x.Id})");
                }
                else
                {
                    _logger.LogInformation($"Ready Join error. Already connected to {x.Name} ({x.Id})");
                }
            }
        }

    }
}