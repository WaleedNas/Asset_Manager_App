using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FAMPro.Data;
using FAMPro.Models;
using Microsoft.AspNetCore.Authorization;

namespace FAMPro.Pages.Assets
{
    [Authorize(Policy = "MustBeEmployee")]
    [Authorize(AuthenticationSchemes = "MyCookieAuth")]
    public class CreateModel : PageModel
    {
        private readonly FAMPro.Data.FAMProContext _context;

        public CreateModel(FAMPro.Data.FAMProContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["LocationId"] = new SelectList(_context.Set<LocationMaster>(), "LocationId", "LocationId");
            return Page();
        }

        [BindProperty]
        public FixedAssetsMaster FixedAssetsMaster { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.FixedAssetsMaster == null || FixedAssetsMaster == null)
            {
                return Page();
            }
            var x = User.Identity.Name;
            FixedAssetsMaster.CreatedDate = DateTime.Now;
            FixedAssetsMaster.CreatedUser = x;
            FixedAssetsMaster.LastEditedDate = DateTime.Now;
            FixedAssetsMaster.LastEditedUser = User.Identity.Name;
            _context.FixedAssetsMaster.Add(FixedAssetsMaster);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
