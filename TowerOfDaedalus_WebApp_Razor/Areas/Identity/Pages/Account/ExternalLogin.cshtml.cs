// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using static TowerOfDaedalus_WebApp_Arango.Schema.Documents;
using Discord.Rest;
using System.Linq;
using Discord;
using Microsoft.Build.Construction;
using TowerOfDaedalus_WebApp_Razor.Properties;

namespace TowerOfDaedalus_WebApp_Razor.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ExternalLoginModel : PageModel
    {
        private readonly SignInManager<Users> _signInManager;
        private readonly UserManager<Users> _userManager;
        private readonly IUserStore<Users> _userStore;
        private readonly RoleManager<Roles> _roleManager;
        private readonly IRoleStore<Roles> _roleStore;
        private readonly IUserEmailStore<Users> _emailStore;
        private readonly ILogger<ExternalLoginModel> _logger;
        private readonly DiscordRestClient _client;

        public ExternalLoginModel(
            SignInManager<Users> signInManager,
            UserManager<Users> userManager,
            IUserStore<Users> userStore,
            RoleManager<Roles> roleManager,
            IRoleStore<Roles> roleStore,
            ILogger<ExternalLoginModel> logger,
            DiscordRestClient client)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userStore = userStore;
            _roleManager = roleManager;
            _roleStore = roleStore;
            _emailStore = (IUserEmailStore<Users>)GetEmailStore();
            _logger = logger;
            _client = client;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ProviderDisplayName { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
        }

        public IActionResult OnGet() => RedirectToPage("./Login");

        public IActionResult OnPost(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
        {
            _logger.LogDebug("OnGetCallbackAsync || onGetCallbackAync triggered");
            returnUrl = returnUrl ?? Url.Content("~/");
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            // Sign in the user with this external login provider if the user already has a login.
            _logger.LogDebug("OnGetCallbackAsync || checking if the user has an existing login");
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                _logger.LogInformation("OnGetCallbackAsync || {Name} logged in with {LoginProvider} provider.", info.Principal.Identity.Name, info.LoginProvider);
                if (ulong.TryParse(Properties.Resources.targetGuildID, out ulong guildId))
                {
                    await _client.LoginAsync(Discord.TokenType.Bearer, info.AuthenticationTokens.First().Value);
                    RestGuildUser guildUser = await _client.GetCurrentUserGuildMemberAsync(guildId);
                    if (guildUser != null)
                    {
                        Users user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
                        if (user != null)
                        {

                            var userClaims = await _userManager.GetClaimsAsync(user);
                            if (userClaims != null)
                            {
                                List<string> discordRoles = new List<string>();
                                foreach (ulong role in guildUser.RoleIds)
                                {
                                    discordRoles.Add(role.ToString());
                                }
                                foreach (var claim in userClaims)
                                {
                                    if (discordRoles.Contains(claim.Value))
                                    {
                                        discordRoles.Remove(claim.Value);
                                    }
                                    else
                                    {
                                        var removeResult = await _userManager.RemoveClaimAsync(user, claim);
                                        if (removeResult.Succeeded)
                                        {
                                            _logger.LogInformation("OnGetCallbackAsync || removed a role from the user");
                                        }
                                    }
                                }

                                if (discordRoles.Any())
                                {
                                    foreach (string role in discordRoles)
                                    {
                                        if (!userClaims.Where(i => i.Value == role).Any())
                                        {
                                            Claim newClaim = new Claim(Resources.customClaim, role);
                                            await _userManager.AddClaimAsync(user, newClaim);
                                        }
                                    }

                                }
                            }

                        }
                    }
                }

                return LocalRedirect(returnUrl);
            }
            if (result.IsLockedOut)
            {
                _logger.LogDebug("OnGetCallbackAsync || user is locked out");
                return RedirectToPage("./Lockout");
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ReturnUrl = returnUrl;
                ProviderDisplayName = info.ProviderDisplayName;
                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    Input = new InputModel
                    {
                    };
                }
                return Page();
            }
        }

        public async Task<IActionResult> OnPostConfirmationAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            // Get the information about the user from the external login provider
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information during confirmation.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            if (ModelState.IsValid)
            {
                _logger.LogDebug("OnGetCallbackAsync || creating user");
                var user = CreateUser();

                _logger.LogDebug("OnGetCallbackAsync || setting token to rest api call header");
                await _client.LoginAsync(Discord.TokenType.Bearer, info.AuthenticationTokens.First().Value);

                _logger.LogDebug("OnGetCallbackAsync || getting user information from discord");
                RestSelfUser discordUser = await _client.GetCurrentUserAsync();

                _logger.LogDebug("OnGetCallbackAsync || setting information from discord to user account");
                user.Id = discordUser.Id.ToString() + discordUser.Discriminator;
                user.DiscordAvatar = discordUser.GetAvatarUrl(Discord.ImageFormat.WebP);

                _logger.LogDebug("OnGetCallbackAsync || setting user name");
                await _userStore.SetUserNameAsync(user, discordUser.Username + discordUser.Discriminator, CancellationToken.None);
                _logger.LogDebug("OnGetCallbackAsync || Setting email");
                await _emailStore.SetEmailAsync(user, discordUser.Email, CancellationToken.None);
                _logger.LogDebug("OnGetCallbackAsync || setting email confirmed");
                await _emailStore.SetEmailConfirmedAsync(user, true, CancellationToken.None);

                _logger.LogDebug("OnGetCallbackAsync || creating user");
                var createResult = await _userManager.CreateAsync(user);
                if (createResult.Succeeded)
                {
                    _logger.LogDebug("OnGetCallbackAsync || User creation succeeded");
                    createResult = await _userManager.AddLoginAsync(user, info);
                    if (createResult.Succeeded)
                    {
                        _logger.LogInformation("OnGetCallbackAsync || User created an account using {Name} provider.", info.LoginProvider);

                        _logger.LogDebug("OnGetCallbackAsync || getting user id");
                        var userId = await _userManager.GetUserIdAsync(user);
                        _logger.LogDebug("OnGetCallbackAsync || generating email confirmation token");
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                        if (ulong.TryParse(Properties.Resources.targetGuildID, out ulong guildId))
                        {
                            RestGuildUser guildUser = await _client.GetCurrentUserGuildMemberAsync(guildId);
                            if (guildUser != null)
                            {

                                List<string> roles = new List<string>();
                                foreach (ulong item in guildUser.RoleIds)
                                {
                                    roles.Add(item.ToString());
                                }

                                var userClaims = await _userManager.GetClaimsAsync(user);
                                foreach (string role in roles)
                                {
                                    
                                    if (!userClaims.Where(i => i.Value == role).Any())
                                    {
                                        Claim newClaim = new Claim(Resources.customClaim, role);
                                        await _userManager.AddClaimAsync(user, newClaim);
                                    }
                                }

                            }
                        }

                        _logger.LogDebug("OnGetCallbackAsync || setting callback url");
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = userId, code = code },
                            protocol: Request.Scheme);

                        _logger.LogDebug("OnGetCallbackAsync || signing in user");
                        await _signInManager.SignInAsync(user, isPersistent: false, info.LoginProvider);
                        _logger.LogDebug("OnGetCallbackAsync || redirecting to [{returnurl}]", returnUrl);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in createResult.Errors)
                {
                    _logger.LogDebug("OnGetCallbackAsync || Error encountered: {errorDescription}", error.Description);
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            ProviderDisplayName = info.ProviderDisplayName;
            ReturnUrl = returnUrl;
            return Page();
        }

        private Users CreateUser()
        {
            try
            {
                return new Users();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(Users)}'. " +
                    $"Ensure that '{nameof(Users)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the external login page in /Areas/Identity/Pages/Account/ExternalLogin.cshtml");
            }
        }

        private IUserEmailStore<Users> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<Users>)_userStore;
        }
    }
}
