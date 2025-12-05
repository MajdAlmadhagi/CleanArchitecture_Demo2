using SCleanArchitecture.SimpleAPI.Domain.Entities;

namespace SCleanArchitecture.SimpleAPI.Domain.Repositories;

public interface IProductRepository
{
    //CRUD Oeration ,also additional methods if we have
    Task AddProductAsync(Product product);
    Task<List<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(int id);

}

