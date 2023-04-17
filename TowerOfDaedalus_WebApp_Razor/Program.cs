using Microsoft.AspNetCore.Identity;
using TowerOfDaedalus_WebApp_Arango.Identity;
using static TowerOfDaedalus_WebApp_Arango.Schema.Documents;
using TowerOfDaedalus_WebApp_Arango;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TowerOfDaedalus_WebApp_Razor.Properties;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo("log4net.config"));
builder.Logging.AddLog4Net();

// Add services to the container.
builder.Services.AddScoped<IArangoUtils, Utilities>();

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

using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;

    var arangoUtils = services.GetRequiredService<IArangoUtils>();
    arangoUtils.CreateDB();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

// Specify health check route for docker
app.MapHealthChecks("/healthz");

app.Run();
