namespace CSharp.Managers.ViewModels
{
    public class Order
    {
        public decimal OrderTotal { get; set; }

        public Customer Customer { get; set; } = new Customer();
    }
}
