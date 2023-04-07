namespace RestaurantService.Models;

public record RestaurantDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<MealDto> Meals { get; set; }
}