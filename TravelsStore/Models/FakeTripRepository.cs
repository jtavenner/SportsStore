using System.Collections.Generic;
using System.Linq;

namespace TravelsStore.Models
{
    public class FakeTripRepository : ITripRepository
    {
        public IQueryable<Trip> Trips => new List<Trip>
        {
            new Trip { Name = "Paris", Price = 400 },
            new Trip { Name = "New York", Price = 500 },
            new Trip { Name = "Istanbul", Price = 600 }
        }.AsQueryable<Trip>();
    }
}
