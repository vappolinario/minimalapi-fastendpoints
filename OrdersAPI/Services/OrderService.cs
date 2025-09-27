using Microsoft.EntityFrameworkCore;

public class OrderService : IOrderService
{
    private readonly AppDbContext _context;

    public OrderService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Order> CreateOrderAsync(Order request, CancellationToken cancellationToken = default)
    {
        await _context.Orders.AddAsync(request, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return request;
    }

    public async Task<IEnumerable<Order>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Orders.ToListAsync(cancellationToken);
    }

    public async Task<Order?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Orders.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }
}

