using MealService.Models;

namespace MealService.Services;

public interface IMealService
{
    Task<List<MealDto>> GetMealsAsync(Guid restaurantId, CancellationToken cancellationToken = default);
}

public sealed class MealService : IMealService
{
    private static List<MealDto> s_meals = new()
    {
        new()
        {
            Id = new("914c1cc5-e9d1-4fa7-a108-1ddce9e1e581"),
            RestaurantId = new("5d99988b-d282-4a49-9efa-59cd32c1a404"),
            Name = "Pizza Margherita",
            Prize = 20
        },
        new()
        {
            Id = new("067a7405-c37c-45d0-80bb-476f8357c6f3"),
            RestaurantId = new("5d99988b-d282-4a49-9efa-59cd32c1a404"),
            Name = "Pizza Capriciosa",
            Prize = 25
        },
        new()
        {
            Id = new("dd7d0d4f-d398-4db5-9d34-82d4fa87f819"),
            RestaurantId = new("ad8e81a9-e260-4f0d-bbf2-52508231e8b1"),
            Name = "Cheeseburger",
            Prize = 23
        },
    };

    public Task<List<MealDto>> GetMealsAsync(Guid restaurantId, CancellationToken cancellationToken = default) => 
        Task.FromResult(s_meals.Where(x => x.RestaurantId == restaurantId).ToList());
}