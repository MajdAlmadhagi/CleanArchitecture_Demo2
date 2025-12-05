using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application.Converters;

internal static class WishlistConverter
{
    public static WishlistItem ToWishlistItemEntity(this AddWishlistItemDto requestDto)//for Add
    {
        return new WishlistItem
        {
            ProductId=requestDto.ProductId,
            

        };



    }

    public static WishlistItemDto ToWishlistItemDto(this WishlistItem WishlistItem)//for Adding response
    {
        return new WishlistItemDto
        {
            ProductId=WishlistItem.ProductId,
        

        };

    }


    public static WishlistDto ToWishlistDto(this Wishlist WishlistObject)//for Get
    {
        
        return new WishlistDto
        {
            Id=WishlistObject.Id,
            UserId=WishlistObject.UserId,
            Items=WishlistObject.Items.Select(i => new WishlistItemDto
        {
            ProductId = i.ProductId,
            //Quantity = i.Quantity,
            //Price=i.Product?.Price ?? 0,
        }).ToList(),


        };



    }



         //Add dependency injection

    public static IServiceCollection AddWishlistService(this IServiceCollection services)
    {
        services.AddScoped<IWishlistService,WishlistService>();
        return services;

    }





}
