using RestaurantService.Extensions;
using RestaurantService.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IRestaurantService, RestaurantService.Services.RestaurantService>();
builder.Services.AddControllers();

// builder.Services.AddRawMealServiceClient();

// builder.Services.AddMealServiceClientWithRetry();

// builder.Services.AddMealServiceClientWithExponentialBackoffRetry();

builder.Services.AddMealServiceClientWithExponentialBackoffRetryAndCircuitBreaker();

var app = builder.Build();

app.MapControllers();

app.Run();

