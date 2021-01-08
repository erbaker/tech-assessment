using CSharp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharp.Repository
{
    public class OrderRepository
    {
        private static List<Order> _orders = new List<Order>()
        {
            new Order() {
                Id = Guid.Parse("b4d6a21e-1e72-4df4-b5de-30a0b3477df6"),
                Customer = "Austin",
                Items = {"Hammer", "Carpet", "Book"}
            },
            new Order() {
                Id = Guid.Parse("b4d6a21e-1e72-4df4-b5de-30a0b3477df7"),
                Customer = "Austin",
                Items = {"Toy", "Blanket", "Car"}
            },
            new Order() {
                Id = Guid.Parse("b4d6a21e-1e72-4df4-b5de-30a0b3477df8"),
                Customer = "Jordan",
                Items = {"Paint"}
            },
            new Order() {
                Id = Guid.Parse("b4d6a21e-1e72-4df4-b5de-30a0b3477df9"),
                Customer = "Greg",
                Items = {"Pencil", "Computer"}
            },
            new Order() {
                Id = Guid.Parse("b4d6a21e-1e72-4df4-b5de-30a0b3477dfa"),
                Customer = "Keegan",
                Items = {"Belt", "Shirt", "Hat"}
            }
        };
        public List<Order> orders
        {
            get { return _orders; }
            set { _orders = value; }
        }

    }
}
