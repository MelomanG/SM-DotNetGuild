using RestaurantService.Models;

namespace RestaurantService.Clients;

public interface IMealServiceClient
{
    Task<List<MealDto>> GetMealsAsync(Guid restaurantId, CancellationToken cancellationToken = default);
}

public sealed class MealServiceClient : IMealServiceClient
{
    private readonly HttpClient _httpClient;
    
    public MealServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<MealDto>> GetMealsAsync(Guid restaurantId, CancellationToken cancellationToken = default)
    {
        using var response = await _httpClient.GetAsync($"meals/{restaurantId}", cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Unable to fetch meals for restaurant {restaurantId}");
        }
        
        return (await response.Content.ReadFromJsonAsync<List<MealDto>>(cancellationToken: cancellationToken))!;
    }
}