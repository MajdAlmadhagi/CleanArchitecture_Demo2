using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Repositories;

public interface IWishlistRepository
{
    
    Task<Wishlist?> ViewWishlistByUserIdAsync(int userId);//بمعنى قد ترجع كائن او null ?
    Task AddWishlistItemAsync(int userId,int productId);

    Task RemoveWishlistItemAsync(int userId,int productId);

    Task ClearWishlistAsync(int userId);


    Task AddWishlistAsync(Wishlist Wishlist);

    Task SaveChangesAsync();


}
