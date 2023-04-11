using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace TowerOfDaedalus_WebApp_Razor.Pages
{
    /// <summary>
    /// A page that acts as the home page of the website
    /// </summary>
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="logger">the logger used to log messages</param>
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// method called whenever a get request is executed
        /// </summary>
        public void OnGet()
        {
            _logger.LogDebug("index get called");
        }
    }
}