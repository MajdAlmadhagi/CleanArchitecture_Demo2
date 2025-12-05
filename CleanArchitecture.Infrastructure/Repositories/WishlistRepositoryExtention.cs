using CleanArchitecture.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.Repositories;

internal static class WishlistRepositoryExtention
{
    public static IServiceCollection AddWishlistRepository(this IServiceCollection services)
    {
        
        services.AddScoped<IWishlistRepository,WishlistRepository>();
        return services;



    }


}
