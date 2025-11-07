using System.ComponentModel.DataAnnotations;

namespace SCleanArchitecture.SimpleAPI.Application.DTOs;

public sealed class AddUserRequestDto
{
    public string Name { get; set; }
    public string Email { get; set; }

    public bool IsValid()
    {
        if (string.IsNullOrEmpty(Name))
        {
            return false;
        }

        if (string.IsNullOrEmpty(Email))
        {
            return false;
        }

        return true;
    }
}
