using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FAMPro.Data;
using FAMPro.Models;
using Microsoft.AspNetCore.Authorization;

namespace FAMPro.Pages.Location
{
    [Authorize(Policy = "MustBeEmployee")]
    [Authorize(AuthenticationSchemes = "MyCookieAuth")]
    public class EditModel : PageModel
    {
        private readonly FAMPro.Data.FAMProContext _context;

        public EditModel(FAMPro.Data.FAMProContext context)
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

            var locationmaster =  await _context.LocationMaster.FirstOrDefaultAsync(m => m.LocationId == id);
            if (locationmaster == null)
            {
                return NotFound();
            }
            LocationMaster = locationmaster;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(LocationMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationMasterExists(LocationMaster.LocationId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool LocationMasterExists(string id)
        {
          return (_context.LocationMaster?.Any(e => e.LocationId == id)).GetValueOrDefault();
        }
    }
}
