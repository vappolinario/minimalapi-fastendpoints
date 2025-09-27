using FastEndpoints;

public class OrderCreateEndpoint : Endpoint<CreateOrderRequestDto, OrderDto, OrderMapper>
{
    private readonly IOrderService _service;

    public OrderCreateEndpoint(IOrderService service)
    {
        _service = service;
    }

    public override void Configure()
    {
        Post("/api/orders");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateOrderRequestDto req, CancellationToken ct)
    {
        var orderRequest = Map.ToEntity(req);
        var order = await _service.CreateOrderAsync(orderRequest);
        Response = Map.FromEntity(order);
        await Send.OkAsync(Response);
    }
}
