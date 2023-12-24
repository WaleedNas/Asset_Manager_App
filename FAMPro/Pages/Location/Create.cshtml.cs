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

namespace FAMPro.Pages.Location
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
            return Page();
        }

        [BindProperty]
        public LocationMaster LocationMaster { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.LocationMaster == null || LocationMaster == null)
            {
                return Page();
            }
          var a = _context.LocationMaster.Where(x => x.LocationId == LocationMaster.LocationId).ToList();
            if (a.Count >= 1)
            {
                return RedirectToPage("/Error");
            }

            _context.LocationMaster.Add(LocationMaster);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
