using System.Collections.Generic;
using TravelsStore.Models;

namespace TravelsStore.Models.ViewModels
{
    public class TripListViewModel
    {
        public IEnumerable<Trip> Trips { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}
