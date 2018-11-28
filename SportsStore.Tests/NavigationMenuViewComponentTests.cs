using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Routing;
using Moq;
using TravelsStore.Components;
using TravelsStore.Models;
using Xunit;

namespace TravelsStore.Tests
{
    public class NavigationMenuViewComponentTests
    {
        [Fact]
        public void Can_Select_Categories()
        {
            //arrange
            Mock<ITripRepository> mock = new Mock<ITripRepository>();
            mock.Setup(m => m.Trips).Returns((new Trip[]
            {
                new Trip {TripID = 1, Name = "P1", Category = "Apples"},
                new Trip {TripID = 2, Name = "P2", Category = "Apples"},
                new Trip {TripID = 3, Name = "P3", Category = "Plums"},
                new Trip {TripID = 4, Name = "P4", Category = "Oranges"},
            }).AsQueryable<Trip>());

            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);

            //Act
            string[] results = ((IEnumerable<string>)(target.Invoke() as ViewViewComponentResult).ViewData.Model).ToArray();

            //Assert
            Assert.True(Enumerable.SequenceEqual(new string[] { "Apples", "Oranges", "Plums" }, results));
        }

        [Fact]
        public void Indicates_Selected_Category()
        {
            //Arrange
            string categoryToSelect = "Apples";

            Mock<ITripRepository> mock = new Mock<ITripRepository>();
            mock.Setup(m => m.Trips).Returns((new Trip[]
            {
                new Trip {TripID = 1, Name = "P1", Category = "Apples"},
                new Trip {TripID = 4, Name = "P2", Category = "Oranges"},
            }).AsQueryable<Trip>());

            NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);
            target.ViewComponentContext = new ViewComponentContext
            {
                ViewContext = new ViewContext
                {
                    RouteData = new RouteData()
                }
            };

            target.RouteData.Values["category"] = categoryToSelect;

            //Act
            string result = (string)(target.Invoke() as ViewViewComponentResult).ViewData["SelectedCategory"];

            //Assert
            Assert.Equal(categoryToSelect, result);

        }
    }
}
