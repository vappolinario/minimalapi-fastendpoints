using FastEndpoints;
using OrdersAPI.Dtos;
using OrdersAPI.Mapping;
using OrdersAPI.Services;

namespace OrdersAPI.Endpoints
{
    public class OrderCreateEndpoint(IOrderService service, ILogger<OrderCreateEndpoint> logger) : Endpoint<CreateOrderRequest, OrderDto, OrderMapper>
    {
        private readonly IOrderService _service = service;
        private readonly ILogger<OrderCreateEndpoint> _logger = logger;

        public override void Configure()
        {
            Post("/api/orders");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CreateOrderRequest req, CancellationToken ct)
        {
            var orderRequest = Map.ToEntity(req);
            var order = await _service.CreateOrderAsync(orderRequest);
            Response = Map.FromEntity(order);
            _logger.LogInformation("Created order {NewOrder}", Response);
            await Send.OkAsync(Response);
        }
    }
}
