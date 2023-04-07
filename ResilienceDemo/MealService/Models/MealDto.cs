namespace MealService.Models;

public record MealDto
{
    public Guid Id { get; set; }
    public Guid RestaurantId { get; set; }
    public string Name { get; set; }
    public decimal Prize { get; set; }
}