using CleanArchitecture.Application.DTOs;

namespace SCleanArchitecture.SimpleAPI.Application.Enums;

public enum AddCartItemStatus
{
    productNotFoundOrOutOfStock,
    cartNotFound,

    success

}
//لم نستخدم enum مباشرة وهذا لانه لابد ان يحتوي فقط على قيم ثابته وليس كلاسات وهكذا
public  class AddCartItemResult//من هذا الكلاس سوف ترجع 4 قيم
{
    
    public CartItemDto cartItemDto{get; set;}
    public AddCartItemStatus status{get; set;}// ال 3 حالات

    

}