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

            // Check if the customer exists, if so, append the customer id so their orders can be linked.
            var existingCustomer = Orders
                .Where(existingOrder => existingOrder.Customer.FirstName == order.Customer.FirstName &&
                                        existingOrder.Customer.LastName == order.Customer.LastName)
                .Select(existingOrder => existingOrder.Customer)
                .FirstOrDefault();

            // If they are an existing customer, then use their information with the CustomerID provided.
            if (existingCustomer != null)
            {
                order.Customer = existingCustomer;
            }
            else
            {
                var existingCustomers = Orders
                    .Select(existingOrders => existingOrders.Customer)
                    .ToList();

                var nextCustomerId = existingCustomers
                    .OrderBy(customer => customer.CustomerId)
                    .Last()
                    .CustomerId;

                order.Customer.CustomerId = nextCustomerId + 1;
            }


            Orders.Add(order);

            return true;
        }

        public List<Order> GetOrders(int customerId)
        {
            return Orders
                .Where(order => order.Customer.CustomerId == customerId)
                .ToList();
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
                    CustomerId = 1,
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
                    CustomerId = 1,
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
                    CustomerId = 2,
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
