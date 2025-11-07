using Microsoft.Extensions.DependencyInjection;
using SCleanArchitecture.SimpleAPI.Application.DTOs;
using SCleanArchitecture.SimpleAPI.Application.Services;
using SCleanArchitecture.SimpleAPI.Domain.Entities;

namespace SCleanArchitecture.SimpleAPI.Application.Converters;

internal static class UserConverter
{
    public static User ToUserEntity(this AddUserRequestDto requestDto)
    {
        return new User
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

    //Add dependency injection
    public static IServiceCollection AddUserService(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();

        return services;
    }

}