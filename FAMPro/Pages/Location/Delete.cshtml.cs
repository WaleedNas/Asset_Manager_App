using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FAMPro.Data;
using FAMPro.Models;
using Microsoft.AspNetCore.Authorization;

namespace FAMPro.Pages.Location
{
    [Authorize(Policy = "MustBeEmployee")]
    [Authorize(AuthenticationSchemes = "MyCookieAuth")]
    public class DeleteModel : PageModel
    {
        private readonly FAMPro.Data.FAMProContext _context;

        public DeleteModel(FAMPro.Data.FAMProContext context)
        {
            _context = context;
        }

        [BindProperty]
      public LocationMaster LocationMaster { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null || _context.LocationMaster == null)
            {
                return NotFound();
            }

            var locationmaster = await _context.LocationMaster.FirstOrDefaultAsync(m => m.LocationId == id);

            if (locationmaster == null)
            {
                return NotFound();
            }
            else 
            {
                LocationMaster = locationmaster;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null || _context.LocationMaster == null)
            {
                return NotFound();
            }
            var locationmaster = await _context.LocationMaster.FindAsync(id);

            if (locationmaster != null)
            {
                LocationMaster = locationmaster;
                _context.LocationMaster.Remove(LocationMaster);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
