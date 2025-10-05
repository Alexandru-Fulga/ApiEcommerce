using System.ComponentModel.DataAnnotations;

namespace ApiEcommerce.Models.Dtos;

public class UpdateProductDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }
    public string ImgUrl { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    [Range(0, int.MaxValue)]
    public int Stock { get; set; }
    public int CategoryId { get; set; }
}
