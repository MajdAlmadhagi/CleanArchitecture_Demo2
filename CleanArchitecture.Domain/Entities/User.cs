using CleanArchitecture.Domain.Entities;

namespace SCleanArchitecture.SimpleAPI.Domain.Entities;

public sealed class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt => DateTime.Now; // when create object will take curren date time for CreateAt property

    //Navigation property(علاقة المستخدم بجدول السلة)
    //One-to-one Relationship
    public Cart Cart{get; set;}//المستخدم الواحد يكون له سلة واحدة 

     //Navigation property(علاقة المستخدم بجدول المفضلات)
    //One-to-one Relationship
    public Wishlist Wishlist{get; set;}//المستخدم الواحد يكون له سلة واحدة 
}
