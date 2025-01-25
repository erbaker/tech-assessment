using CSharp.Models;

namespace CSharp.Repositories;

public interface ICustomerRepository
{
    Task<Customer?> GetCustomerAsync(string customerId);
    Task<IEnumerable<Customer>> GetAllCustomersAsync();
}