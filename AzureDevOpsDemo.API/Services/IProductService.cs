using AzureDevOpsDemo.API.Models;

namespace AzureDevOpsDemo.API.Services;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(int id);
    Task<Product> CreateAsync(CreateProductRequest request);
}