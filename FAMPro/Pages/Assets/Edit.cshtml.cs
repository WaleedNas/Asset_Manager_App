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
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FAMPro.Pages.Assets
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
        public FixedAssetsMaster FixedAssetsMaster { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.FixedAssetsMaster == null)
            {
                return NotFound();
            }

            var fixedassetsmaster =  await _context.FixedAssetsMaster.FirstOrDefaultAsync(m => m.FixedAssetId == id);
            if (fixedassetsmaster == null)
            {
                return NotFound();
            }
            FixedAssetsMaster = fixedassetsmaster;
           ViewData["LocationId"] = new SelectList(_context.Set<LocationMaster>(), "LocationId", "LocationId");
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

            FixedAssetsMaster.LastEditedDate = DateTime.Now;
            FixedAssetsMaster.LastEditedUser = User.Identity.Name;
            

            _context.Attach(FixedAssetsMaster).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FixedAssetsMasterExists(FixedAssetsMaster.FixedAssetId))
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

        private bool FixedAssetsMasterExists(int id)
        {
          return (_context.FixedAssetsMaster?.Any(e => e.FixedAssetId == id)).GetValueOrDefault();
        }
    }
}
