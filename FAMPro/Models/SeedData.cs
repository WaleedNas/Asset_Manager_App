using Microsoft.EntityFrameworkCore;
using FAMPro.Data;

namespace FAMPro.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new FAMProContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<FAMProContext>>()))
            {
                if (context == null || context.LocationMaster == null)
                {
                    throw new ArgumentNullException("Null RazorPagesMovieContext");
                }

                // Look for any movies.
                if (context.LocationMaster.Any())
                {
                    return;   // DB has been seeded
                }

                context.LocationMaster.AddRange(
                    new LocationMaster
                    {
                        LocationId = "SHJ",
                        LocationName = "Sharjah"
                    },

                    new LocationMaster
                    {
                        LocationId = "DXB",
                        LocationName = "Dubai"
                    },

                    new LocationMaster
                    {
                        LocationId = "AJM",
                        LocationName = "Ajman"
                    },

                    new LocationMaster
                    {
                        LocationId = "AUH",
                        LocationName = "Abu Dhabi"
                    },
                    new LocationMaster
                    {
                        LocationId = "AAN",
                        LocationName = "Al Ain"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}