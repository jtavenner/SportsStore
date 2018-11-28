using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TravelsStore.Models;
using TravelsStore.Models.ViewModels;

namespace TravelsStore.Controllers
{
    public class CartController : Controller
    {
        private ITripRepository repository;
        private Cart cart;

        public CartController(ITripRepository repo, Cart cartService)
        {
            repository = repo;
            cart = cartService;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToActionResult AddToCart(int tripID, string returnUrl)
        {
            Trip trip = repository.Trips
                .FirstOrDefault(p => p.TripID == tripID);

            if (trip != null)
            {
                cart.AddItem(trip, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int tripID, string returnUrl)
        {
            Trip trip = repository.Trips
                .FirstOrDefault(p => p.TripID == tripID);

            if (trip != null)
            {
                cart.RemoveLine(trip);
            }

            return RedirectToAction("Index", new { returnUrl });
        }       
    }
}
