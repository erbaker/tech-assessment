using CSharp.Accessors.DataTransferObjects;

namespace CSharp.Accessors
{
    public class OrderAccessor : IOrderAccessor
    {
        public bool CreateOrder(Order order)
        {
            // If this were setup to be controlled through a db, this could be an identity insert for automatic incrementing.
            // For this exercise, get the number of items in the current list of orders and add one for the next OrderId.
            var nextOrderId = Orders
                .Count() + 1;

            order.OrderId = nextOrderId;
            order.IsActive = true;

            Orders.Add(order);

            return true;
        }

        /// <summary>
        /// Allows for a running list of orders to be tracked for the sake of this exercise. Time permitting
        /// this could be controlled through a database provider such as Entity Framework.
        /// </summary>
        private List<Order> Orders = new List<Order>
        {
            new Order
            {
                OrderId = 1,
                IsActive = true,
                OrderTotal = 250.00m,
                Customer = new Customer
                {
                    FirstName = "Test",
                    LastName = "Testerson",
                    Age = 30,
                    Address = new Address
                    {
                        State = "NE",
                        City = "Lincoln",
                        PostalCode = "68522",
                        StreetAddress = "123 Addrss Way"
                    },
                    PhoneNumber = "9876543210"
                }
            },
            new Order
            {
                OrderId = 2,
                IsActive = true,
                OrderTotal = 1000.00m,
                Customer = new Customer
                {
                    FirstName = "Test",
                    LastName = "Testerson",
                    Age = 30,
                    Address = new Address
                    {
                        State = "NE",
                        City = "Lincoln",
                        PostalCode = "68522",
                        StreetAddress = "123 Addrss Way"
                    },
                    PhoneNumber = "9876543210"
                }
            },
            new Order
            {
                OrderId = 3,
                IsActive = true,
                OrderTotal = 45.00m,
                Customer = new Customer
                {
                    FirstName = "Peach",
                    LastName = "Dog",
                    Age = 3,
                    Address = new Address
                    {
                        State = "NE",
                        City = "Lincoln",
                        PostalCode = "68522",
                        StreetAddress = "987 Puppy way"
                    },
                    PhoneNumber = "5554449999"
                }
            }
        };
    }
}
