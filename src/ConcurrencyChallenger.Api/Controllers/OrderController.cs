using ConcurrencyChallenger.Api.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConcurrencyChallenger.Api.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController(AppDbContext db, ILogger<OrderController> logger) : ControllerBase
{
    [HttpPost("{productId}")]
    public async Task<IActionResult> AddOrderAsync(int productId)
    {
        var product = await db.Products.FindAsync(productId);
        if (product == null)
            return NotFound();

        if (product.Stock <= 0)
            return BadRequest("Out of stock");

        try
        {
            product.Stock--;

            // Simulate delay (as if multiple users buying at once)
            logger.LogInformation("Adding order...");
            await Task.Delay(500);
            logger.LogInformation("Adding order done.");

            await db.SaveChangesAsync();

            if (product.Stock < 0)
            {
                throw new Exception("Invalid stock!!");
            }

            return Ok("Purchase successful");
        }
        catch (DbUpdateConcurrencyException ex)
        {
            logger.LogWarning(ex, "Concurrency conflict on Product ID {ProductId}", productId);
            return Conflict("Purchase failed due to concurrent updates.");
        }
    }
}
