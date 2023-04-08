using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using TowerOfDaedalus_WebApp_Blazor.Areas.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Net;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.UI.Services;
using log4net;
using Humanizer.Localisation;
using TowerOfDaedalus_WebApp_Arango;
using TowerOfDaedalus_WebApp_Blazor.Properties;
using Resources = TowerOfDaedalus_WebApp_Blazor.Properties.Resources;
using TowerOfDaedalus_WebApp_Arango.Identity;
using static TowerOfDaedalus_WebApp_Arango.Schema.Documents;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo("log4net.config"));
builder.Logging.AddLog4Net();

// Add services to the container.


builder.Services.AddIdentity<Users, Roles>()
    .AddDefaultTokenProviders();

builder.Services.AddTransient<IUserStore<Users>, ArangoUserStore>();
builder.Services.AddTransient<IRoleStore<Roles>, ArangoRoleStore>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

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
    policy.RequireClaim(Resources.customClaim, Resources.RoleIdAdmin));

    options.AddPolicy("gameMasters", policy =>
    policy.RequireClaim(Resources.customClaim, Resources.RoleIdAssistantGameMaster, Resources.RoleIdGameMaster, Resources.RoleIdAdmin));

    options.AddPolicy("allCharacters", policy =>
    policy.RequireClaim(Resources.customClaim, Resources.RoleIdScholar, Resources.RoleIdScribe, Resources.RoleIdAdvisor, Resources.RoleIdVisitor, Resources.RoleIdClockworkSoldier, Resources.RoleIdAssistantGameMaster, Resources.RoleIdGameMaster, Resources.RoleIdAdmin));

    options.AddPolicy("allPlayers", policy =>
    policy.RequireClaim(Resources.customClaim, Resources.RoleIdScholar, Resources.RoleIdScribe, Resources.RoleIdAdvisor, Resources.RoleIdVisitor, Resources.RoleIdAssistantGameMaster, Resources.RoleIdGameMaster, Resources.RoleIdAdmin));

    options.AddPolicy("permanentPlayers", policy =>
    policy.RequireClaim(Resources.customClaim, Resources.RoleIdScholar, Resources.RoleIdScribe, Resources.RoleIdAssistantGameMaster, Resources.RoleIdGameMaster, Resources.RoleIdAdmin));

    options.AddPolicy("visitingPlayers", policy =>
    policy.RequireClaim(Resources.customClaim, Resources.RoleIdAdvisor, Resources.RoleIdVisitor, Resources.RoleIdAssistantGameMaster, Resources.RoleIdGameMaster, Resources.RoleIdAdmin));

    options.AddPolicy("nonPlayerCharacters", policy =>
    policy.RequireClaim(Resources.customClaim, Resources.RoleIdClockworkSoldier, Resources.RoleIdAssistantGameMaster, Resources.RoleIdGameMaster, Resources.RoleIdAdmin));

    options.AddPolicy("viewers", policy =>
    policy.RequireClaim(Resources.customClaim, Resources.RoleIdSpectralWatcher, Resources.RoleIdAdmin));

    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddScoped<IArangoUtils, Utilities>();

// Add health checks to report to docker
builder.Services.AddHealthChecks();

var app = builder.Build();

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

app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

// Specify health check route for docker
app.MapHealthChecks("/healthz");

app.Run();
