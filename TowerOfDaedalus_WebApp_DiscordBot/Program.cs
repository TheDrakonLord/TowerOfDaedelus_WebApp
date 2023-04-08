using TowerOfDaedalus_WebApp_DiscordBot;
using TowerOfDaedalus_WebApp_DiscordBot.Properties;
using TowerOfDaedalus_WebApp_Arango;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo("log4net.config"));
builder.Logging.AddLog4Net();

builder.Services.AddScoped<IArangoUtils, Utilities>();

builder.Services.AddHostedService<DiscordBot>();

builder.Services.Configure<DiscordBotOptions>(options =>
{
    options.botToken = Environment.GetEnvironmentVariable("DISCORD_BOT_TOKEN");
    options.targetPublicChannel = Resources.targetPublicChannel;
    options.targetGMChannel = Resources.targetGMChannel;
    options.targetServer = Resources.targetGuildID;
});

// Add health checks to report to docker
builder.Services.AddHealthChecks()
    .AddCheck<DockerHealthCheck>("Docker");

var app = builder.Build();

// Specify health check route for docker
app.MapHealthChecks("/healthz");

app.Run();
