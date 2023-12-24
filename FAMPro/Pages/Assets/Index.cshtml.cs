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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FAMPro.Pages.Assets
{
#pragma warning disable CS8618
#pragma warning disable CS8604
    [Authorize(Policy ="MustBeEmployee")]
    [Authorize(AuthenticationSchemes ="MyCookieAuth")]
    public class IndexModel : PageModel
    {
        private readonly FAMPro.Data.FAMProContext _context;

        public IndexModel(FAMPro.Data.FAMProContext context)
        {
            _context = context;
        }

        public IList<FixedAssetsMaster> FixedAssetsMaster { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.FixedAssetsMaster != null)
            {
                FixedAssetsMaster = await _context.FixedAssetsMaster
                .Include(f => f.locationMaster).ToListAsync();
            }

            IQueryable<string> locationQuery = from m in _context.FixedAssetsMaster
                                            orderby m.LocationId
                                            select m.LocationId;

            var assets = from m in _context.FixedAssetsMaster
                         select m;

            if (!string.IsNullOrEmpty(AssetLocation))
            {
                assets = assets.Where(x => x.LocationId == AssetLocation);
            }

            if (DateTime.Compare(FromDate,ToDate) <= 0)
            {
                assets = assets.Where(x => x.AcquiredDate <= ToDate && x.AcquiredDate >= FromDate);
            }

            Locations = new SelectList(await locationQuery.Distinct().ToListAsync());
            FixedAssetsMaster = await assets.ToListAsync();


        }

        public SelectList Locations { get; set; }
        [BindProperty(SupportsGet = true)]
        public string AssetLocation { get; set; }
        [BindProperty(SupportsGet = true)]
        public DateTime FromDate { get; set; } = DateTime.MinValue;
        [BindProperty(SupportsGet = true)]
        public DateTime ToDate { get; set; } = DateTime.Now;
    }
#pragma warning restore CS8618
#pragma warning restore CS8604
}
