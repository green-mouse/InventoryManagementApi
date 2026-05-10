using System.ComponentModel.DataAnnotations;

namespace InventoryManagementApi.DTOs;

public class CreateCategoryDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
}