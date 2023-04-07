using RestaurantService.Clients;
using RestaurantService.Models;

namespace RestaurantService.Services;

public interface IRestaurantService
{
    Task<RestaurantDto> GetRestaurantAsync(Guid restaurantId, CancellationToken cancellationToken = default);
}

public sealed class RestaurantService : IRestaurantService
{
    private static List<RestaurantDto> s_restaurants = new()
    {
        new()
        {
            Id = new("5d99988b-d282-4a49-9efa-59cd32c1a404"),
            Name = "Pizza House"
        },
        new()
        {
            Id = new("ad8e81a9-e260-4f0d-bbf2-52508231e8b1"),
            Name = "Pirate Burger"
        }
    };
    
    private readonly IMealServiceClient _mealServiceClient;

    public RestaurantService(IMealServiceClient mealServiceClient)
    {
        _mealServiceClient = mealServiceClient;
    }

    public async Task<RestaurantDto> GetRestaurantAsync(Guid restaurantId, CancellationToken cancellationToken = default)
    {
        var restaurant = s_restaurants.First(x => x.Id == restaurantId);
        var meals = await _mealServiceClient.GetMealsAsync(restaurantId, cancellationToken);
        return restaurant with
        {
            Meals = meals
        };
    }
}