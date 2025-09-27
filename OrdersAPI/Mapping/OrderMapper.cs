using FastEndpoints;

public class OrderMapper : Mapper<CreateOrderRequestDto, OrderDto, Order>
{
    public override Order ToEntity(CreateOrderRequestDto request)
    {
        return new Order
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Status = "Created",
            TotalCost = request.TotalCost
        };
    }

    public override OrderDto FromEntity(Order order)
    {
        return new OrderDto
        (
            order.Id,
            order.FirstName,
            order.LastName,
            order.Status,
            order.TotalCost
        );

    }
}
