using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FAMPro.Models;

namespace FAMPro.Data
{
    public class FAMProContext : DbContext
    {
        public FAMProContext (DbContextOptions<FAMProContext> options)
            : base(options)
        {
        }

        public DbSet<FAMPro.Models.FixedAssetsMaster>? FixedAssetsMaster { get; set; }

        public DbSet<FAMPro.Models.LocationMaster>? LocationMaster { get; set; }

        public DbSet<FAMPro.Models.Credential>? Credential { get; set; }
    }
}
