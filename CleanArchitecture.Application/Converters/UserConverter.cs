using Microsoft.Extensions.DependencyInjection;
using SCleanArchitecture.SimpleAPI.Application.DTOs;
using SCleanArchitecture.SimpleAPI.Application.Services;
using SCleanArchitecture.SimpleAPI.Domain.Entities;

namespace SCleanArchitecture.SimpleAPI.Application.Converters;

internal static class UserConverter
{
    public static User ToUserEntity(this AddUserRequestDto requestDto)
    {
        return new User //المتغيرات المراد ارسالها للدومين فقط
        {
            Name= requestDto.Name, 
            Email = requestDto.Email,
        };
    }
    
    public static AddUserResponseDto ToAddUserResponse(this AddUserRequestDto requestDto, DateTime createdAt)
    {
        return new AddUserResponseDto
        {
            Name= requestDto.Name,
            Email = requestDto.Email,
            CreatedAt = createdAt,
        };
    }

    public static GetUserDto ToUserDto(this User userObject)
    {
        return new GetUserDto
        {
            Id=userObject.Id,
            Name= userObject.Name,
            Email = userObject.Email,
            CreatedAt = userObject.CreatedAt,
        };
    }






    //Add dependency injection
    public static IServiceCollection AddUserService(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();

        return services;
    }

    /*يعرّف النظام كيف ينشئ كائنًا من 
         *  
         * UserServices
         * في ملف ال
         * controller
         * 
         * بما ان الcontroller    
         * services تتعامل مباشرة مع ال
         * وهذا ملف نفس فكرة ملف
         * UserRepositoryExtention
         */




}