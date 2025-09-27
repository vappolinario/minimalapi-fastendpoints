public interface IOrderService
{
    public Task<Order> CreateOrderAsync(Order request);
    public Task<IEnumerable<Order>> GetAllAsync();
    public Task<Order?> GetByIdAsync(int id);
}
