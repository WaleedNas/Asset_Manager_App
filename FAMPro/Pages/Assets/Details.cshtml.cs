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
    public class DetailsModel : PageModel
    {
        private readonly FAMPro.Data.FAMProContext _context;

        public DetailsModel(FAMPro.Data.FAMProContext context)
        {
            _context = context;
            _context.FixedAssetsMaster.Include(x => x.locationMaster).ToList();
        }

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
    }
}
