using CleanArchitecture.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.Repositories;

internal static class CartRepositoryExtention
{
    public static IServiceCollection AddCartRepository(this IServiceCollection services)
    {
        
        services.AddScoped<ICartRepository,CartRepository>();
        return services;



    }


}
