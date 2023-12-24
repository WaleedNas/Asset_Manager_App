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

namespace FAMPro.Pages.AccountManagement
{
    [Authorize(Policy = "MustBeAdmin")]
    [Authorize(AuthenticationSchemes = "MyCookieAuth")]
    public class DeleteModel : PageModel
    {
        private readonly FAMPro.Data.FAMProContext _context;

        public DeleteModel(FAMPro.Data.FAMProContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Credential Credential { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Credential == null)
            {
                return NotFound();
            }

            var credential = await _context.Credential.FirstOrDefaultAsync(m => m.UserID == id);

            if (credential == null)
            {
                return NotFound();
            }
            else 
            {
                Credential = credential;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Credential == null)
            {
                return NotFound();
            }
            var credential = await _context.Credential.FindAsync(id);

            if (credential != null)
            {
                Credential = credential;
                _context.Credential.Remove(Credential);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
