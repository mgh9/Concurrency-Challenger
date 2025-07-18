using System.ComponentModel.DataAnnotations;

namespace ConcurrencyChallenger.Api.Models;

public class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;

    public int Stock { get; set; }
}
