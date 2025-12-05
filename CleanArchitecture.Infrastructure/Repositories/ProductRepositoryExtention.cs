namespace CleanArchitecture.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using SCleanArchitecture.SimpleAPI.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

internal static class ProductRepositoryExtention
{
    public static IServiceCollection AddProductRepository(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository,ProductRepository>();
        return services;


    }



}
