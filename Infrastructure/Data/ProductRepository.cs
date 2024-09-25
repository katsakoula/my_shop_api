using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProductRepository(StoreContext context) : IProductRepository
{

    public void AddProduct(Product Product)
    {
        context.Products.Add(Product);
    }

    public void DeleteProduct(Product Product)
    {
        context.Products.Remove(Product);
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await context.Products.FindAsync(id);
    }
    public async Task<IReadOnlyList<string>> GetBrandsAsync()
    {
        return await context.Products.Select(x => x.Brand).Distinct().ToListAsync();
    }
    public async Task<IReadOnlyList<string>> GetTypesAsync()
    {
        return await context.Products.Select(x => x.Type).Distinct().ToListAsync();
    }
    public async Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort)
    {
        var query = context.Products.AsQueryable();
        if (!string.IsNullOrWhiteSpace(brand))
            query = query.Where(x => x.Brand == brand);
        if (!string.IsNullOrWhiteSpace(type))
            query = query.Where(x => x.Type == type);

        query = sort switch
        {
            "priceAcs" => query.OrderBy(x => x.Price),
            "priceDesc" => query.OrderByDescending(x => x.Price),
            _ => query.OrderBy(x => x.Name)
        };

        return await query.ToListAsync();
    }
    public bool ProductExists(int id)
    {
        return context.Products.Any(x => x.Id == id);
    }
    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }
    public void UpdateProduct(Product Product)
    {
        context.Entry(Product).State = EntityState.Modified;
    }
}
