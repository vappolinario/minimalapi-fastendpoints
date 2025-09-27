using FastEndpoints;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("BaseConnection")));

builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddFastEndpoints();
var app = builder.Build();

app.UseFastEndpoints();

app.Run();
