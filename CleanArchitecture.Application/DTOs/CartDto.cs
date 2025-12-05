namespace CleanArchitecture.Application.DTOs;

public class CartDto
{
    
    public int Id {get; set;} //CartId
    public int UserId{get; set;}//F.K to User Entity
    public List<CartItemDto> Items{get; set;}=new List<CartItemDto>();//السلة الواحدة لها اكثر من عنصر او منتج

    public decimal TotalPrice{get; set;}

}

public class CartItemDto
{
    public int ProductId {get; set;} //F.k to Product Entity 
    public int Quantity {get; set;} //Quantity of this Item

     //public double Price { get; set; } // أضافيه فقط للاستعلام في السواجر


}
