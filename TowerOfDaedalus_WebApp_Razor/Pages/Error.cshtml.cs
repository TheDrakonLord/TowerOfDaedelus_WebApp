using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace TowerOfDaedelus_WebApp.Pages
{
    /// <summary>
    /// a page that is displayed whenever a error occurs
    /// </summary>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        /// <summary>
        /// the primary key of the request that had an error
        /// </summary>
        public string? RequestId { get; set; }

        /// <summary>
        /// whether or not the requestId should be displayed
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        private readonly ILogger<ErrorModel> _logger;

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="logger">the logger used to log messages</param>
        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// method that is executed whenever a get request is recieved
        /// obtains and siplayes the request ID
        /// </summary>
        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}