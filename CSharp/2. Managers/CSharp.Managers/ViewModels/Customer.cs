namespace CSharp.Managers.ViewModels
{
    public class Customer
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public int Age { get; set; } = 0;

        public Address Address { get; set; } = new Address();

        public string PhoneNumber { get; set; } = string.Empty;
    }
}
