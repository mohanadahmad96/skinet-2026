using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort);
    Task<Product?> GetProductByIdAsync(int id);

    Task AddProductAsync(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);

    Task<bool> ProductExistsAsync(int id);
    Task<bool> SaveChangesAsync();
    Task<IReadOnlyList<string>> GetBrandsAsync();
    Task<IReadOnlyList<string>> GetTypesAsync();
}