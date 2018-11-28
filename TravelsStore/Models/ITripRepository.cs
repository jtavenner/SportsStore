using System.Linq;

namespace TravelsStore.Models
{
    public interface ITripRepository
    {
        IQueryable<Trip> Trips {  get; }
    }
}
