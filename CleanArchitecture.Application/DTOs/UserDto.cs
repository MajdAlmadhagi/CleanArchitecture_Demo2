namespace SCleanArchitecture.SimpleAPI.Application.DTOs;

public class GetUserDto //for GetAllUsers & GetUserById
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }

}
