namespace CleanArchitecture.Application.DTOs;

public class AddCartItemDto
{
    
//الاشياء التي سيتم تمريرها عند الاضافة للسلة

//public int CartId {get; set;} //F.K to Cart Entity becuase those items belong to Cart

public int ProductId {get; set;} //F.k to Product Entity 
public int Quantity {get; set;} //Quantity of this Item

//public double Price { get; set; } // أضافيه فقط للاستعلام في السواجر



}
