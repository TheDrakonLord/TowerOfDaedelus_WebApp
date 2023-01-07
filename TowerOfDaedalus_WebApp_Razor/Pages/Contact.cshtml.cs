using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace TowerOfDaedelus_WebApp.Pages
{
    /// <summary>
    /// a page that displays the contact information of the development team
    /// </summary>
    [AllowAnonymous]
    public class ContactModel : PageModel
    {
        /// <summary>
        /// a method that is executed whenever a get request is recieved
        /// </summary>
        public void OnGet()
        {
        }
    }
}
