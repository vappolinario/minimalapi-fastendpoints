using FastEndpoints;

public class GetOrdersEndpoint : EndpointWithoutRequest<IEnumerable<OrderDto>, OrderMapper>
{
    private readonly IOrderService _service;

    public GetOrdersEndpoint(IOrderService service)
    {
        _service = service;
    }

    public override void Configure()
    {
        Get("/api/orders");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var orders = await _service.GetAllAsync();
        Response = orders.Select( o => Map.FromEntity(o));
        await Send.OkAsync(Response);
    }
}
