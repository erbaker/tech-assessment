namespace CSharp.Accessors
{
    public class OrderAccessor : IOrderAccessor
    {
        public async Task<bool> CreateOrderAsync()
        {
            return await Task.Run(() => true);
        }
    }
}
