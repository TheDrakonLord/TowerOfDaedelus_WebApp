#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TowerOfDaedelus_WebApp.Data;
using TowerOfDaedelus_WebApp.Models;

namespace TowerOfDaedelus_WebApp.Pages.CharacterSheets
{
    public class IndexModel : PageModel
    {
        private readonly TowerOfDaedelus_WebApp.Data.ApplicationDbContext _context;

        public IndexModel(TowerOfDaedelus_WebApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<CharSheet> CharSheet { get;set; }

        public async Task OnGetAsync()
        {
            CharSheet = await _context.CharSheet.ToListAsync();
        }
    }
}
