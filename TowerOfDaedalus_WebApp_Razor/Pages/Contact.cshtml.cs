using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace TowerOfDaedelus_WebApp.Pages
{
    [AllowAnonymous]
    public class ContactModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
