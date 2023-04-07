using Microsoft.AspNetCore.Mvc;
using RestaurantService.Services;

namespace RestaurantService.Controllers;

[ApiController]
[Route("restaurants")]
public sealed class RestaurantsController : ControllerBase
{
    private readonly IRestaurantService _restaurantService;

    public RestaurantsController(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    [HttpGet("{restaurantId:guid}")]
    public async Task<IActionResult> GetRestaurantAsync([FromRoute] Guid restaurantId, CancellationToken cancellationToken)
    {
        var result = await _restaurantService.GetRestaurantAsync(restaurantId, cancellationToken);
        return Ok(result);
    }
}