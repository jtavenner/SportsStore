using System.Linq;
using TravelsStore.Models;
using Xunit;

namespace TravelsStore.Tests
{
    public class CartTests
    {
        [Fact]
        public void Can_Add_New_Lines()
        {
            //arrange
            Trip p1 = new Trip { TripID = 1, Name = "P1" };
            Trip p2 = new Trip { TripID = 2, Name = "P2" };

            Cart target = new Cart();

            //Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            CartLine[] results = target.Lines.ToArray();

            //assert
            Assert.Equal(2, results.Length);
            Assert.Equal(p1, results[0].Trip);
            Assert.Equal(p2, results[1].Trip);
        }

        [Fact]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            //arrange
            Trip p1 = new Trip { TripID = 1, Name = "P1" };
            Trip p2 = new Trip { TripID = 2, Name = "P2" };

            Cart target = new Cart();

            //Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 10);
            CartLine[] results = target.Lines
                   .OrderBy(c => c.Trip.TripID).ToArray();

            //assert
            Assert.Equal(2, results.Length);
            Assert.Equal(11, results[0].Quantity);
            Assert.Equal(1, results[1].Quantity);
        }

        [Fact]
        public void Can_Remove_Line()
        {
            //arrange
            Trip p1 = new Trip { TripID = 1, Name = "P1" };
            Trip p2 = new Trip { TripID = 2, Name = "P2" };
            Trip p3 = new Trip { TripID = 3, Name = "P3" };

            Cart target = new Cart();            
            target.AddItem(p1, 1);
            target.AddItem(p2, 3);
            target.AddItem(p3, 5);
            target.AddItem(p2, 1);


            //act
            target.RemoveLine(p2);

            //assert
            //Assert.Equal(0, target.Lines.Where(c => c.Trip == p2).Count());
            Assert.Empty(target.Lines.Where(c => c.Trip == p2));
            Assert.Equal(2, target.Lines.Count());
        }

        [Fact]
        public void Calculate_Cart_Total()
        {
            //arrange
            Trip p1 = new Trip { TripID = 1, Name = "P1", Price = 100M };
            Trip p2 = new Trip { TripID = 2, Name = "P2", Price = 50M};

            Cart target = new Cart();


            //Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);
            decimal result = target.ComputeTotalValue();


            //assert           
            Assert.Equal(450m, result);
        }

        [Fact]
        public void Can_Clear_Contents()
        {
            //arrange
            Trip p1 = new Trip { TripID = 1, Name = "P1", Price = 100M };
            Trip p2 = new Trip { TripID = 2, Name = "P2", Price = 50M };

            Cart target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);

            //act
            target.Clear();


            //assert           
            Assert.Empty(target.Lines);
        }
    }
}
