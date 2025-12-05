using CleanArchitecture.Application.Converters;
using Microsoft.Extensions.DependencyInjection;
using SCleanArchitecture.SimpleAPI.Application.Converters;

namespace SCleanArchitecture.SimpleAPI.Application.Extensions;

public static class AddDependencyInjection ////ملف يتم فيه تجميع الخدمات التي في طبقة ال applications
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddUserService();
        services.AddProductService();
        services.AddCartService();
        services.AddWishlistService();

        return services;
    }
}
