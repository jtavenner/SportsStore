using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using TravelsStore.Controllers;
using TravelsStore.Models;
using TravelsStore.Models.ViewModels;

namespace TravelsStore.Tests
{
    public class TripControllerTests
    {
        [Fact]
        public void Can_Paginate()
        {
            //Arrange
            Mock<ITripRepository> mock = new Mock<ITripRepository>();
            mock.Setup(m => m.Trips).Returns((new Trip[]
            {
                new Trip{TripID = 1, Name = "P1"},
                new Trip{TripID = 2, Name = "P2"},
                new Trip{TripID = 3, Name = "P3"},
                new Trip{TripID = 4, Name = "P4"},
                new Trip{TripID = 5, Name = "P5"}
            }).AsQueryable<Trip>());

            TripController controller = new TripController(mock.Object);
            controller.PageSize = 3;

            //Act
            TripListViewModel result = controller.List(null, 2).ViewData.Model as TripListViewModel;

            //Assert
            Trip[] prodArray = result.Trips.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("P4", prodArray[0].Name);
            Assert.Equal("P5", prodArray[1].Name);
        }

        [Fact]
        public void Can_Send_Pagination_View_Model()
        {
            //arrange
            Mock<ITripRepository> mock = new Mock<ITripRepository>();
            mock.Setup(m => m.Trips).Returns((new Trip[]
                {
                    new Trip{TripID = 1, Name = "P1"},
                    new Trip{TripID = 2, Name = "P2"},
                    new Trip{TripID = 3, Name = "P3"},
                    new Trip{TripID = 4, Name = "P4"},
                    new Trip{TripID = 5, Name = "P5"}
                }).AsQueryable<Trip>());

            TripController controller = new TripController(mock.Object) { PageSize = 3 };

            //Act
            TripListViewModel result = controller.List(null, 2).ViewData.Model as TripListViewModel;

            //Assert
            PagingInfo pageInfo = result.PagingInfo;

            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerpage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);

        }

        [Fact]
        public void Can_Filter_Trips()
        {
            //arrange
            Mock<ITripRepository> mock = new Mock<ITripRepository>();
            mock.Setup(m => m.Trips).Returns((new Trip[]
                {
                    new Trip{TripID = 1, Name = "P1", Category = "Cat1"},
                    new Trip{TripID = 2, Name = "P2", Category = "Cat2"},
                    new Trip{TripID = 3, Name = "P3", Category = "Cat1"},
                    new Trip{TripID = 4, Name = "P4", Category = "Cat2"},
                    new Trip{TripID = 5, Name = "P5", Category = "Cat3"}
                }).AsQueryable<Trip>());

            TripController controller = new TripController(mock.Object) { PageSize = 3 };

            //Act
            Trip[] result = 
                (controller.List("Cat2", 1).ViewData.Model as TripListViewModel)
                    .Trips.ToArray();

            //Assert
            Assert.Equal(2, result.Length);
            Assert.True(result[0].Name == "P2" && result[0].Category == "Cat2");
            Assert.True(result[1].Name == "P4" && result[1].Category == "Cat2");
        }

        [Fact]
        public void Generate_Category_Specific_Trip_Count()
        {
            //arrange
            Mock<ITripRepository> mock = new Mock<ITripRepository>();
            mock.Setup(m => m.Trips).Returns((new Trip[]
                {
                    new Trip{TripID = 1, Name = "P1", Category = "Cat1"},
                    new Trip{TripID = 2, Name = "P2", Category = "Cat2"},
                    new Trip{TripID = 3, Name = "P3", Category = "Cat1"},
                    new Trip{TripID = 4, Name = "P4", Category = "Cat2"},
                    new Trip{TripID = 5, Name = "P5", Category = "Cat3"}
                }).AsQueryable<Trip>());

            TripController target = new TripController(mock.Object) { PageSize = 3 };

            Func<ViewResult, TripListViewModel> GetModel = result =>
                result?.ViewData?.Model as TripListViewModel;

            //act
            int? res1 = GetModel(target.List("Cat1"))?.PagingInfo.TotalItems;
            int? res2 = GetModel(target.List("Cat2"))?.PagingInfo.TotalItems;
            int? res3 = GetModel(target.List("Cat3"))?.PagingInfo.TotalItems;
            int? resAll = GetModel(target.List(null))?.PagingInfo.TotalItems;

            //assert
            Assert.Equal(2, res1);
            Assert.Equal(2, res2);
            Assert.Equal(1, res3);
            Assert.Equal(5, resAll);
        }
    }
}
