using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TowerOfDaedelus_WebApp.Data;
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

const string targetGuildID = "866469546109173792";
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

        options.Events = new OAuthEvents
        {
            OnCreatingTicket = async context =>
            {
                HttpWebRequest webRequest1 = (HttpWebRequest)WebRequest.Create($"https://discordapp.com/api/users/@me/guilds/{targetGuildID}/member");
                webRequest1.Method = "Get";
                webRequest1.ContentLength = 0;
                webRequest1.Headers.Add("Authorization", "Bearer " + context.AccessToken);
                webRequest1.ContentType = "application/x-www-form-urlencoded";

                string[] resultArray;
                using (HttpWebResponse response1 = webRequest1.GetResponse() as HttpWebResponse)
                {
                    using (StreamReader reader = new StreamReader(response1.GetResponseStream()))
                    {
                        string jString = reader.ReadToEnd();
                        JObject jsonObject = JObject.Parse(jString);
                        JToken jRoles = jsonObject.SelectToken("roles");
                        resultArray = jRoles.ToObject<string[]>();
                    }
                }

                foreach (string x in resultArray)
                {
                    Claim y = new Claim(customClaim, x);
                    context.Identity.AddClaim(y);
                    
                }
            }
        };

        options.SaveTokens = true;
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admins", policy =>
    policy.RequireClaim(customClaim,RoleIdAdmin));

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
