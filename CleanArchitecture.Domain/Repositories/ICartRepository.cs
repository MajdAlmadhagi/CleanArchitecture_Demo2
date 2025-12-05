using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Repositories;

public interface ICartRepository
{
    
    Task<Cart?> ViewCartByUserIdAsync(int userId);//بمعنى قد ترجع كائن او null ?
    Task AddCartItemAsync(int userId,int productId,int quantity);

    Task RemoveCartItemAsync(int userId,int productId);

    Task ClearCartAsync(int userId);


    Task AddCartAsync(Cart cart);

    Task SaveChangesAsync();


}
