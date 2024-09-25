using System;
using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort);
    Task<Product> GetProductByIdAsync(int id);
    Task<IReadOnlyList<string>> GetBrandsAsync();
    Task<IReadOnlyList<string>> GetTypesAsync();


    void AddProduct(Product Product);
    void UpdateProduct(Product Product);
    void DeleteProduct(Product Product);
    bool ProductExists(int id);

    Task<bool> SaveChangesAsync();
}
