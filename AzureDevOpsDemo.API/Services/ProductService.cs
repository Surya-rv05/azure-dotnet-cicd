using AzureDevOpsDemo.API.Models;

namespace AzureDevOpsDemo.API.Services;

public class ProductService : IProductService
{
    private static readonly List<Product> _products = new()
    {
        new Product { Id = 1, Name = "Laptop", Description = "High performance laptop", Price = 75000, StockQuantity = 10 },
        new Product { Id = 2, Name = "Mouse", Description = "Wireless mouse", Price = 1500, StockQuantity = 50 },
        new Product { Id = 3, Name = "Keyboard", Description = "Mechanical keyboard", Price = 3500, StockQuantity = 30 }
    };

    public Task<IEnumerable<Product>> GetAllAsync() =>
        Task.FromResult(_products.AsEnumerable());

    public Task<Product?> GetByIdAsync(int id) =>
        Task.FromResult(_products.FirstOrDefault(p => p.Id == id));

    public Task<Product> CreateAsync(CreateProductRequest request)
    {
        var product = new Product
        {
            Id = _products.Max(p => p.Id) + 1,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            StockQuantity = request.StockQuantity
        };
        _products.Add(product);
        return Task.FromResult(product);
    }
}