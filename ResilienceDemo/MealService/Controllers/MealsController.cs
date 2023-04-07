using MealService.Services;
using Microsoft.AspNetCore.Mvc;

namespace MealService.Controllers;

[ApiController]
[Route("meals")]
public sealed class MealsController : ControllerBase
{
    private readonly IMealService _mealService;

    public MealsController(IMealService mealService)
    {
        _mealService = mealService;
    }

    [HttpGet("{restaurantId:guid}")]
    public async Task<IActionResult> GetMealsAsync([FromRoute] Guid restaurantId, CancellationToken cancellationToken)
    {
        var random = new Random();
        if (random.Next(1, 3) == 2)
        {
            throw new Exception("Something went wrong!");
        }
        
        var meals = await _mealService.GetMealsAsync(restaurantId, cancellationToken);
        
        return Ok(meals);
    }
}