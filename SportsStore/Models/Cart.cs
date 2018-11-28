using System.Collections.Generic;
using System.Linq;

namespace TravelsStore.Models
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public virtual void AddItem(Trip trip, int quantity)
        {
            CartLine line = lineCollection
                .Where(p => p.Trip.TripID == trip.TripID)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Trip = trip,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Trip trip) =>
            lineCollection.RemoveAll(l => l.Trip.TripID == trip.TripID);

        public virtual decimal ComputeTotalValue() =>
            lineCollection.Sum(e => e.Trip.Price * e.Quantity);

        public virtual void Clear() => lineCollection.Clear();

        public virtual IEnumerable<CartLine> Lines => lineCollection;

    }
    public class CartLine
    {
        public int CartLineID { get; set; }
        public Trip Trip { get; set; }
        public int Quantity { get; set; }
    }
}
