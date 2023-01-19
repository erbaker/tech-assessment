using System;

namespace CSharp_Advanced.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public OrderStatusEnum Status { get; set; }
        public DateTime ReceivedDateTime { get; set; }
        public DateTime LastUpdatedDateTime { get; set; }
    }

    public enum OrderStatusEnum
    {
        Received,
        Shipped,
        Canceled
    }
}
