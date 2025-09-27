public interface IOrderService
{
    public Task<Order> CreateOrderAsync(Order request, CancellationToken cancellationToken = default);
    public Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken = default);
    public Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
