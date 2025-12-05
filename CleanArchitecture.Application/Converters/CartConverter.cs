using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application.Converters;

internal static class CartConverter
{
    public static CartItem ToCartItemEntity(this AddCartItemDto requestDto)//for Adding
    {
        return new CartItem
        {   
            //CartId=requestDto.CartId,
            ProductId=requestDto.ProductId,
            Quantity=requestDto.Quantity,
            

        };



    }

    public static CartItemDto ToCartItemDto(this CartItem cartItem)//for Adding response
    {
        return new CartItemDto
        {
            
            ProductId=cartItem.ProductId,
            Quantity=cartItem.Quantity,
            


        };

    }


    public static CartDto ToCartDto(this Cart cartObject)//for Get
    {
        
        return new CartDto
        {
            Id=cartObject.Id,
            UserId=cartObject.UserId,
            Items=cartObject.Items.Select(i => new CartItemDto
        {
            ProductId = i.ProductId,
            Quantity = i.Quantity,
            //Price=i.Product?.Price ?? 0,
        }).ToList(),


        };



    }



         //Add dependency injection

    public static IServiceCollection AddCartService(this IServiceCollection services)
    {
        services.AddScoped<ICartService,CartService>();
        return services;

    }





}
