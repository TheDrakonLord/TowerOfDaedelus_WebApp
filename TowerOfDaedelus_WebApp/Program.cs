using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TowerOfDaedelus_WebApp.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Net;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authentication;

var builder = WebApplication.CreateBuilder(args);

const string RoleIdAdmin = "866470088508178452";
const string RoleIdGameMaster = "868206725268926504";
const string RoleIdAssistantGameMaster = "868206804931346463";
const string RoleIdScholar = "866469929926393866";
const string RoleIdScribe = "866469956677664768";
const string RoleIdAdvisor = "866470007274078227";
const string RoleIdVisitor = "866470040563482656";
const string RoleIdClockworkSoldier = "868207055620681768";
const string RoleIdSpectralWatcher = "868207008669630485";
const string customClaim = "DiscordRole";

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

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
    policy.RequireClaim(customClaim,RoleIdAdmin));

    options.AddPolicy("gameMasters", policy =>
    policy.RequireClaim(customClaim, RoleIdAssistantGameMaster,RoleIdGameMaster, RoleIdAdmin));

    options.AddPolicy("allCharacters", policy =>
    policy.RequireClaim(customClaim, RoleIdScholar, RoleIdScribe, RoleIdAdvisor, RoleIdVisitor, RoleIdClockworkSoldier, RoleIdAssistantGameMaster, RoleIdGameMaster, RoleIdAdmin));

    options.AddPolicy("allPlayers", policy =>
    policy.RequireClaim(customClaim, RoleIdScholar, RoleIdScribe, RoleIdAdvisor, RoleIdVisitor, RoleIdAssistantGameMaster, RoleIdGameMaster, RoleIdAdmin));

    options.AddPolicy("permanentPlayers", policy =>
    policy.RequireClaim(customClaim, RoleIdScholar, RoleIdScribe, RoleIdAssistantGameMaster, RoleIdGameMaster, RoleIdAdmin));

    options.AddPolicy("visitingPlayers", policy =>
    policy.RequireClaim(customClaim, RoleIdAdvisor, RoleIdVisitor, RoleIdAssistantGameMaster, RoleIdGameMaster, RoleIdAdmin));

    options.AddPolicy("nonPlayerCharacters", policy =>
    policy.RequireClaim(customClaim, RoleIdClockworkSoldier, RoleIdAssistantGameMaster, RoleIdGameMaster, RoleIdAdmin));

    options.AddPolicy("viewers", policy =>
    policy.RequireClaim(customClaim, RoleIdSpectralWatcher, RoleIdAdmin));

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

app.MapRazorPages();

app.Run();


