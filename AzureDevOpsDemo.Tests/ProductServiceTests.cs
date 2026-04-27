using AzureDevOpsDemo.API.Models;
using AzureDevOpsDemo.API.Services;
using FluentAssertions;

namespace AzureDevOpsDemo.Tests;

public class ProductServiceTests
{
    private IProductService CreateFreshService() => new ProductService();

    [Fact]
    public async Task GetAllAsync_ShouldReturn_AtLeastThreeDefaultProducts()
    {
        // Arrange
        var service = CreateFreshService();

        // Act
        var products = await service.GetAllAsync();

        // Assert
        products.Should().NotBeNull();
        products.Should().HaveCountGreaterThanOrEqualTo(3);
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ShouldReturn_CorrectProduct()
    {
        // Arrange
        var service = CreateFreshService();
        var expectedId = 1;

        // Act
        var product = await service.GetByIdAsync(expectedId);

        // Assert
        product.Should().NotBeNull();
        product!.Id.Should().Be(expectedId);
        product.Name.Should().Be("Laptop");
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ShouldReturn_Null()
    {
        // Arrange
        var service = CreateFreshService();

        // Act
        var product = await service.GetByIdAsync(999);

        // Assert
        product.Should().BeNull();
    }

    [Fact]
    public async Task CreateAsync_ShouldAdd_NewProduct()
    {
        // Arrange
        var service = CreateFreshService();
        var request = new CreateProductRequest
        {
            Name = "Monitor",
            Description = "4K Monitor",
            Price = 25000,
            StockQuantity = 15
        };

        // Act
        var product = await service.CreateAsync(request);

        // Assert
        product.Should().NotBeNull();
        product.Id.Should().BeGreaterThan(0);
        product.Name.Should().Be("Monitor");
        product.Price.Should().Be(25000);
    }
}