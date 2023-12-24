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
    [Authorize(Policy ="MustBeAdmin")]
    [Authorize(AuthenticationSchemes ="MyCookieAuth")]
    public class IndexModel : PageModel
    {
        private readonly FAMPro.Data.FAMProContext _context;

        public IndexModel(FAMPro.Data.FAMProContext context)
        {
            _context = context;
        }

        public IList<Credential> Credential { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Credential != null)
            {
                Credential = await _context.Credential.ToListAsync();
            }
        }
    }
}
