/*using InventoryManagementApi.Data;
using InventoryManagementApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        return await _context.Products.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
            return NotFound();

        return product;
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, Product product)
    {
        if (id != product.Id)
            return BadRequest();

        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
            return NotFound();

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}*/
using InventoryManagementApi.Data;
using InventoryManagementApi.DTOs;
using InventoryManagementApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> GetProducts()
    {
        var products = await _context.Products
          .Select(p => new ProductDto
          {
              Id = p.Id,
              Name = p.Name,
              Quantity = p.Quantity,
              Price = p.Price,
              CategoryId = p.CategoryId,
              CategoryName = p.Category != null ? p.Category.Name : string.Empty
          })
            .ToListAsync();

        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        var product = await _context.Products
            .Where(p => p.Id == id)
           .Select(p => new ProductDto
           {
               Id = p.Id,
               Name = p.Name,
               Quantity = p.Quantity,
               Price = p.Price,
               CategoryId = p.CategoryId,
               CategoryName = p.Category != null ? p.Category.Name : string.Empty
           })
            .FirstOrDefaultAsync();

        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> CreateProduct(CreateProductDto request)
    {
        var categoryExists = await _context.Categories.AnyAsync(c => c.Id == request.CategoryId);

        if (!categoryExists)
            return BadRequest("Category does not exist.");
        var product = new Product
        {
            Name = request.Name,
            Quantity = request.Quantity,
            Price = request.Price,
            CategoryId = request.CategoryId
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        var response = new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Quantity = product.Quantity,
            Price = product.Price,
            CategoryId = request.CategoryId
        };

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto request)
    {
        var categoryExists = await _context.Categories.AnyAsync(c => c.Id == request.CategoryId);

        if (!categoryExists)
            return BadRequest("Category does not exist.");
        var product = await _context.Products.FindAsync(id);

        if (product == null)
            return NotFound();

        product.Name = request.Name;
        product.Quantity = request.Quantity;
        product.Price = request.Price;
        product.CategoryId = request.CategoryId;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
            return NotFound();

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    [HttpGet("search")]
    public async Task<ActionResult<List<ProductDto>>> SearchProducts(string name)
    {
        var products = await _context.Products
            .Where(p => p.Name.ToLower().Contains(name.ToLower()))
            .Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Quantity = p.Quantity,
                Price = p.Price,
                CategoryId = p.CategoryId,
                CategoryName = p.Category != null ? p.Category.Name : string.Empty
            })
            .ToListAsync();

        return Ok(products);
    }
}