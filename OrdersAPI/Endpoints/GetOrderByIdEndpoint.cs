using FastEndpoints;
using OrdersAPI.Dtos;
using OrdersAPI.Mapping;
using OrdersAPI.Services;

namespace OrdersAPI.Endpoints
{
    public class GetOrderByIdEndpoint(IOrderService service) : EndpointWithoutRequest<OrderDto, OrderMapper>
    {
        private readonly IOrderService _service = service;

        public override void Configure()
        {
            Get("/api/orders/{id:int}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            int id = Route<int>("id");
            var order = await _service.GetByIdAsync(id);

            if (order == null)
                await Send.NotFoundAsync();
            else
                await Send.OkAsync(Map.FromEntity(order));
        }
    }
}
