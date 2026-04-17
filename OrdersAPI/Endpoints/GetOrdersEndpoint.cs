using FastEndpoints;
using OrdersAPI.Dtos;
using OrdersAPI.Mapping;
using OrdersAPI.Services;

namespace OrdersAPI.Endpoints
{
    public class GetOrdersEndpoint(IOrderService service, ILogger<GetOrdersEndpoint> logger) : EndpointWithoutRequest<IEnumerable<OrderDto>, OrderMapper>
    {
        private readonly IOrderService _service = service;
        private readonly ILogger _logger = logger;

        public override void Configure()
        {
            Get("/api/orders");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var orders = await _service.GetAllAsync();
            Response = orders.Select(o => Map.FromEntity(o));
            _logger.LogInformation("Retrieved {OrdersCount} orders.", orders.Count());
            await Send.OkAsync(Response);
        }
    }
}
