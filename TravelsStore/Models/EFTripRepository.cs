using System.Collections.Generic;
using System.Linq;

namespace TravelsStore.Models
{
    public class EFTripRepository : ITripRepository
    {
        private ApplicationDbContext context;

        public EFTripRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Trip> Trips => context.Trips;
    }
}
