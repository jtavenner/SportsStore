using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TravelsStore.Models;

namespace TravelsStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private ITripRepository repository;

        public NavigationMenuViewComponent(ITripRepository repo)
        {
            repository = repo;
        }
        
        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(repository.Trips
                        .Select(x => x.Category)
                        .Distinct()
                        .OrderBy(x => x));
        }
    }
}
