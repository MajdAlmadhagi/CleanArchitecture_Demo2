using CleanArchitecture.Domain.Entities;

namespace SCleanArchitecture.SimpleAPI.Domain.Entities;

public sealed class Product
{

public int Id {get; set;}
public string Name {get; set;}
public double Price {get; set;}
public string Description {get; set;}

public int Stock {get; set;}//المخزون للمنتج

//   Navigation property(توضح انه هناك علاقة مع جدول CartItem) 

public List< CartItem > CartItems{get; set;}=new List<CartItem>();//المنتج الواحد ممكن يكون لأكثر من عنصر سلة او لاكثر من سلة


public List< WishlistItem > WishlistItems{get; set;}=new List<WishlistItem>();//المنتج الواحد ممكن يكون لأكثر من عنصر مفضلة او لاكثر من سلة


}
/*
		المنتج الواحد (Product) يمكن أن يظهر في عدة عناصر سلة (CartItems) لمستخدمين مختلفين.

لكن كل عنصر سلة (CartItem) مرتبط بمنتج واحد فقط (Product).			
		*/
