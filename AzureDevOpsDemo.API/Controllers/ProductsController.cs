using AzureDevOpsDemo.API.Models;
using AzureDevOpsDemo.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureDevOpsDemo.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IProductService productService, ILogger<ProductsController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Fetching all products");
        var products = await _productService.GetAllAsync();
        return Ok(ApiResponse<IEnumerable<Product>>.Ok(products));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        _logger.LogInformation("Fetching product with id {Id}", id);
        var product = await _productService.GetByIdAsync(id);

        if (product is null)
            return NotFound(ApiResponse<Product>.Fail($"Product with id {id} not found"));

        return Ok(ApiResponse<Product>.Ok(product));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
    {
        _logger.LogInformation("Creating new product: {Name}", request.Name);
        var product = await _productService.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = product.Id, version = "1" },
            ApiResponse<Product>.Ok(product, "Product created successfully"));
    }
}