using MealService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers();

builder.Services.AddSingleton<IMealService, MealService.Services.MealService>();

var app = builder.Build();

app.MapControllers();

app.Run();
