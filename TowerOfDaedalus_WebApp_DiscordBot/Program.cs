using TowerOfDaedalus_WebApp_DiscordBot;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<DiscordBot_BackgroundWorker>();
    })
    .Build();

await host.RunAsync();
