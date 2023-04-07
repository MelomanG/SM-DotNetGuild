namespace RestaurantService.Models;

public record MealDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal Prize { get; set; }
}