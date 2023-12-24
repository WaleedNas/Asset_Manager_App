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

namespace FAMPro.Pages.AccountManagement
{
    [Authorize(Policy = "MustBeAdmin")]
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
        public Credential Credential { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Credential == null || Credential == null)
            {
                return Page();
            }

            _context.Credential.Add(Credential);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
