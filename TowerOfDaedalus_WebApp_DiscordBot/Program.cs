using TowerOfDaedalus_WebApp_DiscordBot;
using log4net;
using Microsoft.Extensions.Options;
using TowerOfDaedalus_WebApp_DiscordBot.Properties;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        logging.AddLog4Net();
    })
    .ConfigureServices(services =>
    {
        services.AddHostedService<DiscordBot_BackgroundWorker>();
    })
    .Build();

await host.RunAsync();
