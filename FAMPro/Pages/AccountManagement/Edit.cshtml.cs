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

namespace FAMPro.Pages.AccountManagement
{
    [Authorize(Policy = "MustBeAdmin")]
    [Authorize(AuthenticationSchemes = "MyCookieAuth")]
    public class EditModel : PageModel
    {
        private readonly FAMPro.Data.FAMProContext _context;

        public EditModel(FAMPro.Data.FAMProContext context)
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

            var credential =  await _context.Credential.FirstOrDefaultAsync(m => m.UserID == id);
            if (credential == null)
            {
                return NotFound();
            }
            Credential = credential;
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

            _context.Attach(Credential).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CredentialExists(Credential.UserID))
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

        private bool CredentialExists(int id)
        {
          return (_context.Credential?.Any(e => e.UserID == id)).GetValueOrDefault();
        }
    }
}
