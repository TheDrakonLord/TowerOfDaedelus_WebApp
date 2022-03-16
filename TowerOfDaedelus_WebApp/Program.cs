using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TowerOfDaedelus_WebApp.Data;
using TowerOfDaedelus_WebApp.Properties;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Net;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authentication;
using TowerOfDaedelus_WebApp.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using TowerOfDaedalus_WebApp_DiscordBot;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromMinutes(3);
});

builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.Configure<AuthMessageSenderOptions>(options =>
{
    options.SendGridKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
});

builder.Services.AddHostedService<DiscordBot_BackgroundWorker>();

builder.Services.Configure<DiscordBotOptions>(options =>
{
    options.botToken = Environment.GetEnvironmentVariable("DISCORD_BOT_TOKEN");
    options.targetPublicChannel = Resources.targetPublicChannel;
    options.targetGMChannel = Resources.targetGMChannel;
    options.targetServer = Resources.targetGuildID;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromDays(1);
    options.SlidingExpiration = true;
});

builder.Services.AddRazorPages();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".TowersOfDaedelus.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

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

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapRazorPages();

app.Run();


