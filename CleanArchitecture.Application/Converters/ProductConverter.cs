using Microsoft.Extensions.DependencyInjection;
using SCleanArchitecture.SimpleAPI.Application.DTOs;
using SCleanArchitecture.SimpleAPI.Application.Services;
using SCleanArchitecture.SimpleAPI.Domain.Entities;


namespace SCleanArchitecture.SimpleAPI.Application.Converters;

internal static class ProductConverter
{


    public static Product ToProductEntity(this AddProductRequestDto requestDto)
    {
        return new Product
        { //المتغيرات المراد ارسالها للدومين فقط
        Name=requestDto.Name,
        Price=requestDto.Price,
        Description=requestDto.Description,
        Stock=requestDto.Stock,



        };




    }


    public static ProductDto ToProductDto(this Product productObject)
    {
        return new ProductDto
        { //المتغيرات المراد استقبالها من الدومين فقط للاستعلام عنها في api
        Id=productObject.Id,
        Name=productObject.Name,
        Price=productObject.Price,
        Description=productObject.Description,
        Stock=productObject.Stock,
        

        /*
        Id=1,
        Name="2",
        Price=2,
        Description="3",
        */

        };




    }


     //Add dependency injection
     //الان في الحقن هذا يتم تسجيل هذه الكلاسات في Service Container للasp.net لكي يتم الوصول للخدمات من تا controller
    public static IServiceCollection AddProductService(this IServiceCollection services)
    {
        services.AddScoped<IProductService,ProductService>();

        return services;
    }

//AddScoped ينشئ كائن جديد لكل طلب HTTP (الأكثر شيوعًا للخدمات).


    






}
