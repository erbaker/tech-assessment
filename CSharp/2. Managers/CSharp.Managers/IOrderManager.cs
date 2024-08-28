namespace CSharp.Managers
{
    public interface IOrderManager
    {
        Task<bool> CreateOrderAsync();
    }
}
