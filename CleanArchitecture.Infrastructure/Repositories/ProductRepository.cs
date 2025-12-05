using SCleanArchitecture.SimpleAPI.Domain.Entities;
using SCleanArchitecture.SimpleAPI.Domain.Repositories;
using CleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace CleanArchitecture.Infrastructure.Repositories;

internal sealed class ProductRepository :IProductRepository  //internal sealed??
{

    private readonly ApplicationDbContext _context;//to deal and access the operations of DbContext Class

    public ProductRepository(ApplicationDbContext context)//?
    {
        _context=context;

    

    }

    //1-
    public async Task AddProductAsync(Product product)
    {
        _context.Products.Add(product);
       await _context.SaveChangesAsync();
       Console.WriteLine($"{product.Id}-{product.Name}-{product.Description},{product.Price}-{product.Stock}");


    }
    //2-
    public async Task<List<Product>> GetAllProductsAsync()
    {
    var DbProducts=  await _context.Products.ToListAsync();
    return DbProducts;

    }
    //3-

    public async Task<Product> GetProductByIdAsync(int id)
    {
      var product= await _context.Products.FindAsync(id);
      return product;


    }

    //4-
    public async Task UpdateProductAsync(Product product)

    {
       var existingProduct=await  _context.Products.FindAsync(product.Id);

       
        existingProduct.Name=product.Name;
        existingProduct.Price=product.Price;
        existingProduct.Description=product.Description;
        existingProduct.Stock=product.Stock;


        _context.Products.Update(existingProduct);
       await _context.SaveChangesAsync();

       Console.WriteLine("*****Product After Update in DB:");
                   Console.WriteLine($"{existingProduct.Name} - {existingProduct.Price}-{existingProduct.Description}-{existingProduct.Stock}");//عند الادخال يتم طباعة ماتم اضافته لقاعدة البيانات



       }


    
    //5-
    public async Task DeleteProductAsync(int id)
    {
     var product= await _context.Products.FindAsync(id);

    _context.Products.Remove(product);
     await _context.SaveChangesAsync();


     }
    

    }

    




