namespace CleanArchitecture.Application.DTOs;

public class WishlistDto
{
    
    public int Id {get; set;} //WishlistId
    public int UserId{get; set;}//F.K to User Entity
    public List<WishlistItemDto> Items{get; set;}=new List<WishlistItemDto>();//السلة الواحدة لها اكثر من عنصر او منتج

}

public class WishlistItemDto
{
    public int ProductId {get; set;} //F.k to Product Entity 

}
