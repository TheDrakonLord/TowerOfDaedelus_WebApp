using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace TowerOfDaedelus_WebApp_Razor.Pages
{
    /// <summary>
    /// A page detailing the privacy policy of the site
    /// </summary>
    [AllowAnonymous]
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="logger">the logger being used to log messages</param>
        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// method called whenever a get request is executed
        /// </summary>
        public void OnGet()
        {
        }
    }
}