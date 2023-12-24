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

namespace FAMPro.Pages.Assets
{
    [Authorize(Policy = "MustBeEmployee")]
    [Authorize(AuthenticationSchemes = "MyCookieAuth")]
#pragma warning disable CS8618
#pragma warning disable CS8601
#pragma warning disable CS8602
#pragma warning disable CS8604
    public class DeleteModel : PageModel
    {
        private readonly FAMPro.Data.FAMProContext _context;

        public DeleteModel(FAMPro.Data.FAMProContext context)
        {
            _context = context;
            _context.FixedAssetsMaster.Include(x => x.locationMaster).ToList();
        }

        [BindProperty]
      public FixedAssetsMaster FixedAssetsMaster { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.FixedAssetsMaster == null)
            {
                return NotFound();
            }

            var fixedassetsmaster = await _context.FixedAssetsMaster.FirstOrDefaultAsync(m => m.FixedAssetId == id);

            if (fixedassetsmaster == null)
            {
                return NotFound();
            }
            else 
            {
                FixedAssetsMaster = fixedassetsmaster;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.FixedAssetsMaster == null)
            {
                return NotFound();
            }
            var fixedassetsmaster = await _context.FixedAssetsMaster.FindAsync(id);

            if (fixedassetsmaster != null)
            {
                FixedAssetsMaster = fixedassetsmaster;
                _context.FixedAssetsMaster.Remove(FixedAssetsMaster);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
#pragma warning restore CS8618
#pragma warning restore CS8601
#pragma warning restore CS8602
#pragma warning restore CS8604
}
