using ConcurrencyChallenger.Api.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace ConcurrencyChallenger.Api.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController(AppDbContext db, ILogger<ProductController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCapacityAsync(int productId)
    {
        var product = await db.Products.FindAsync(productId);
        if (product == null)
            return NotFound();

        logger.LogInformation("Capacity is {Capacity}", product.Stock);

        return Ok(product.Stock);
    }
}