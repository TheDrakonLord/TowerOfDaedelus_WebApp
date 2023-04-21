using Microsoft.AspNetCore.Identity;
using TowerOfDaedalus_WebApp_Arango.Identity;
using static TowerOfDaedalus_WebApp_Arango.Schema.Documents;
using TowerOfDaedalus_WebApp_Arango;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TowerOfDaedalus_WebApp_Razor.Properties;
using Discord.Rest;
using Microsoft.Extensions.DependencyInjection;
using TowerOfDaedalus_WebApp_Kafka;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo("log4net.config"));
builder.Logging.AddLog4Net();

// Add services to the container.
builder.Services.AddScoped<IArangoUtils, TowerOfDaedalus_WebApp_Arango.Utilities>();
builder.Services.AddScoped<IKafkaUtils, TowerOfDaedalus_WebApp_Kafka.Utilities>();
builder.Services.AddScoped<IKafkaProducer, KafkaProducer>();

builder.Services.Configure<KafkaOptions>(options =>
{
    options.BrokerHost = "kafka:9092";
    options.Topics = new List<string>
    {
        KafkaSchema.topicBotCommands,
        KafkaSchema.topicBotEvents,
        KafkaSchema.topicServiceStatus
    };
});

// Add health checks to report to docker
builder.Services.AddHealthChecks();


// Add Identity types
builder.Services.AddIdentity<Users, Roles>()
    .AddDefaultTokenProviders();

// Identity Services
builder.Services.AddTransient<IUserStore<Users>, ArangoUserStore>();
builder.Services.AddTransient<IRoleStore<Roles>, ArangoRoleStore>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddSingleton<DiscordRestClient>();

builder.Services.AddRazorPages();

builder.Services.AddAuthentication()
    .AddDiscord(options =>
    {
        try
        {
            options.ClientId = Environment.GetEnvironmentVariable("DISCORD_CLIENT_ID");
            options.ClientSecret = Environment.GetEnvironmentVariable("DISCORD_CLIENT_SECRET");
        }
        catch (ArgumentNullException)
        {

            throw;
        }

        options.CallbackPath = "/signin-discord";
        options.Scope.Add("guilds.members.read");
        options.Scope.Add("identify");
        options.Scope.Add("email");

        options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email", "string");
        options.ClaimActions.MapJsonKey(ClaimTypes.Name, "username", "string");

        options.SaveTokens = true;
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("admins", policy =>
    policy.RequireClaim(Resources.ResourceManager.GetString("customClaim"), Resources.ResourceManager.GetString("RoleIdAdmin")));

    options.AddPolicy("gameMasters", policy =>
    policy.RequireClaim(Resources.ResourceManager.GetString("customClaim"), Resources.ResourceManager.GetString("RoleIdAssistantGameMaster"), Resources.ResourceManager.GetString("RoleIdGameMaster"), Resources.ResourceManager.GetString("RoleIdAdmin")));

    options.AddPolicy("allCharacters", policy =>
    policy.RequireClaim(Resources.ResourceManager.GetString("customClaim"), Resources.ResourceManager.GetString("RoleIdScholar"), Resources.ResourceManager.GetString("RoleIdScribe"), Resources.ResourceManager.GetString("RoleIdAdvisor"), Resources.ResourceManager.GetString("RoleIdVisitor"), Resources.ResourceManager.GetString("RoleIdClockworkSoldier"), Resources.ResourceManager.GetString("RoleIdAssistantGameMaster"), Resources.ResourceManager.GetString("RoleIdGameMaster"), Resources.ResourceManager.GetString("RoleIdAdmin")));

    options.AddPolicy("allPlayers", policy =>
    policy.RequireClaim(Resources.ResourceManager.GetString("customClaim"), Resources.ResourceManager.GetString("RoleIdScholar"), Resources.ResourceManager.GetString("RoleIdScribe"), Resources.ResourceManager.GetString("RoleIdAdvisor"), Resources.ResourceManager.GetString("RoleIdVisitor"), Resources.ResourceManager.GetString("RoleIdAssistantGameMaster"), Resources.ResourceManager.GetString("RoleIdGameMaster"), Resources.ResourceManager.GetString("RoleIdAdmin")));

    options.AddPolicy("permanentPlayers", policy =>
    policy.RequireClaim(Resources.ResourceManager.GetString("customClaim"), Resources.ResourceManager.GetString("RoleIdScholar"), Resources.ResourceManager.GetString("RoleIdScribe"), Resources.ResourceManager.GetString("RoleIdAssistantGameMaster"), Resources.ResourceManager.GetString("RoleIdGameMaster"), Resources.ResourceManager.GetString("RoleIdAdmin")));

    options.AddPolicy("visitingPlayers", policy =>
    policy.RequireClaim(Resources.ResourceManager.GetString("customClaim"), Resources.ResourceManager.GetString("RoleIdAdvisor"), Resources.ResourceManager.GetString("RoleIdVisitor"), Resources.ResourceManager.GetString("RoleIdAssistantGameMaster"), Resources.ResourceManager.GetString("RoleIdGameMaster"), Resources.ResourceManager.GetString("RoleIdAdmin")));

    options.AddPolicy("nonPlayerCharacters", policy =>
    policy.RequireClaim(Resources.ResourceManager.GetString("customClaim"), Resources.ResourceManager.GetString("RoleIdClockworkSoldier"), Resources.ResourceManager.GetString("RoleIdAssistantGameMaster"), Resources.ResourceManager.GetString("RoleIdGameMaster"), Resources.ResourceManager.GetString("RoleIdAdmin")));

    options.AddPolicy("viewers", policy =>
    policy.RequireClaim(Resources.ResourceManager.GetString("customClaim"), Resources.ResourceManager.GetString("RoleIdSpectralWatcher"), Resources.ResourceManager.GetString("RoleIdAdmin")));

    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});


var app = builder.Build();

app.Logger.LogInformation("Creating database");
using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;

    var arangoUtils = services.GetRequiredService<IArangoUtils>();
    arangoUtils.CreateDB();

    app.Logger.LogInformation("creating kafka topics");
    var kafkaUtils = services.GetRequiredService<IKafkaUtils>();
    kafkaUtils.CreateTopics();
}

// Configure the HTTP request pipeline.
app.Logger.LogInformation("configuring the HTTP request pipeline");
if (app.Environment.IsDevelopment())
{
    app.Logger.LogInformation("Environment is development, using migrations endpoint");
    app.UseMigrationsEndPoint();
}
else
{
    app.Logger.LogInformation("Using exception handler and hsts");
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Logger.LogInformation("setting up https redirection");
app.UseHttpsRedirection();
app.Logger.LogInformation("Using static files");
app.UseStaticFiles();

app.Logger.LogInformation("using routing");
app.UseRouting();

app.Logger.LogInformation("Setting up authentication and authorization");
app.UseAuthentication();
app.UseAuthorization();

app.Logger.LogInformation("Adding routes");
app.MapRazorPages();

// Specify health check route for docker
app.MapHealthChecks("/healthz");

app.Logger.LogInformation("Starting the app");
app.Run();
