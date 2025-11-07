using SCleanArchitecture.SimpleAPI.Application.Converters;
using SCleanArchitecture.SimpleAPI.Application.DTOs;
using SCleanArchitecture.SimpleAPI.Domain.Repositories;

namespace SCleanArchitecture.SimpleAPI.Application.Services;

public interface IUserService
{
    Task<AddUserResponseDto> AddUser(AddUserRequestDto requestDto);
}

internal  class UserService(IUserRepository _userRepository) : IUserService
{

    public async Task<AddUserResponseDto> AddUser(AddUserRequestDto requestDto)
    {
        try
        {
            if (!requestDto.IsValid())
            {
                return UserErrors.InvalidRequest();
            }

            var userEntity = requestDto.ToUserEntity();

            await _userRepository.AddUserAsync(userEntity);

            var response = requestDto.ToAddUserResponse(userEntity.CreatedAt);

            return response;

        }
        catch (Exception ex)
        {
            throw new ArgumentException("Unexpected Error!");
        }
    }
}


public static class UserErrors
{
    public static AddUserResponseDto InvalidRequest()
    {
        return new AddUserResponseDto();
    }
}
