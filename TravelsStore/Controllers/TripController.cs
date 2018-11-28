using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TravelsStore.Models;
using TravelsStore.Models.ViewModels;


namespace TravelsStore.Controllers
{
    public class TripController : Controller
    {
        private ITripRepository repository;
        public int PageSize = 4;

        public TripController(ITripRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string category, int tripPage = 1)
            => View(new TripListViewModel
            {
                Trips = repository.Trips
                .Where(p => p.Category == null || p.Category == category)
                    .OrderBy(p => p.TripID)
                    .Skip((tripPage - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = tripPage,
                    ItemsPerpage = PageSize,
                    TotalItems = category == null ?
                        repository.Trips.Count() :
                        repository.Trips.Where(e => e.Category == category).Count()
                },
                CurrentCategory = category
            });
    }
}
