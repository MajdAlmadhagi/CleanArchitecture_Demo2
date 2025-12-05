using CleanArchitecture.Application.DTOs;

namespace SCleanArchitecture.SimpleAPI.Application.Enums;

public enum AddWishlistItemStatus
{
    productNotFoundOrOutOfStock,
    WishlistNotFound,

    ProductAlreadyExist,
    success,

    

}
//لم نستخدم enum مباشرة وهذا لانه لابد ان يحتوي فقط على قيم ثابته وليس كلاسات وهكذا
public  class AddWishlistItemResult//من هذا الكلاس سوف ترجع 4 قيم
{
    
    public WishlistItemDto WishlistItemDto{get; set;}
    public AddWishlistItemStatus status{get; set;}// ال 3 حالات

    

}