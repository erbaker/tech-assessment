namespace Models
{
    public class Customer
    {
        private int Id { get; set; }
        private string firstName { get; set; }
        private int age { get; set; }
        private Address address { get; set; }
        private string phoneNumber { get; set; }
    }

    class Address
    {
        public string streetAddress { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postalCode { get; set; }
    }
}