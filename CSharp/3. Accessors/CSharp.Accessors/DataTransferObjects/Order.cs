namespace CSharp.Accessors.DataTransferObjects
{
    public class Order
    {
        /// <summary>
        /// Unique identifier for the order record.
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Used to determine if the order is in an active state/being processed.
        /// </summary>
        public bool IsActive { get; set; } = false;

        /// <summary>
        /// Total for the order.
        /// </summary>
        public decimal OrderTotal { get; set; }

        /// <summary>
        /// Customer associated with the current order.
        /// </summary>
        public Customer Customer { get; set; } = new Customer();
    }
}
