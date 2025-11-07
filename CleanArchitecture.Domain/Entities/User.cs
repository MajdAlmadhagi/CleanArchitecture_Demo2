namespace SCleanArchitecture.SimpleAPI.Domain.Entities;

public sealed class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt => DateTime.Now; // when create object will take curren date time for CreateAt property
}
