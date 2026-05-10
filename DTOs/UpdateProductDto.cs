using System.ComponentModel.DataAnnotations;

namespace InventoryManagementApi.DTOs;

public class UpdateProductDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }

    [Range(0.01, double.MaxValue)]
    public decimal Price { get; set; }

    [Range(1, int.MaxValue)]
    public int CategoryId { get; set; }
}