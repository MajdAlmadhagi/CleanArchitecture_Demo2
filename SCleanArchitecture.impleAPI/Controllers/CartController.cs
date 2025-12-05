using CleanArchitecture.Application.Services;
using Microsoft.AspNetCore.Mvc;
using SCleanArchitecture.SimpleAPI.Application.Enums;

namespace SCleanArchitecture.SimpleAPI.Controllers;

[ApiController]
[Route("Cart")]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;
    public CartController(ICartService cartService)
    {
        _cartService=cartService;
    }


    [HttpPost]

    public async Task<ActionResult> AddCartItem(int userId,int productId,int quantity)
    {
        var result= await _cartService.AddCartItem(userId,productId,quantity);


        if (result.status == AddCartItemStatus.productNotFoundOrOutOfStock)
        {
            return BadRequest("Sorry, this Product is not found now- or the stock is not enough!!");
        }

        if(result.status == AddCartItemStatus.success)
        {
            
            return Ok(result.cartItemDto);
        }

        return StatusCode(500, "Unexpected error.");

        /*if(result == null)
        {
            return BadRequest("Invalid CartItem Data");
        }*/

        //return Ok(result);
    }


    [HttpGet]
    public async Task<ActionResult> ViewCartByUserId(int userId)
    {
        var result= await _cartService.ViewCartByUserId(userId);

        if(result == null)
        {
            return NotFound("Favorite Not Found!!");
        }

        return Ok(result);
    }

    [HttpDelete]

    public async Task<ActionResult> RemoveCartItem(int userId, int productId)
    {
         var result= await _cartService.ViewCartByUserId(userId);

           var deleted=await _cartService.RemoveCartItem(userId,productId);

        if (!deleted)
        {
         return  NotFound("Product is't deleted,Product not found..!"); 

        }
        return Ok("Product deleted successfully");//في حال كان المستخدم موجود وتم الحذف
 

    }

    [HttpDelete("Clear")]

    public async Task<ActionResult> ClearCart(int userId)
    {
        
        var result=await _cartService.ClearCart(userId);

        if (result==ClearCartResult.cartNotFound)
        {
            return NotFound("There is no Cart to Clear its products ");
        }

        if (result == ClearCartResult.cartEmpty)
        {
            return BadRequest("This Cart is Empty,no items to clear it");
        }

        if (result == ClearCartResult.cartCleared)
        {
         return Ok("Cart is Cleared Successfully..");

        }

            return StatusCode(500, "Unexpected error.");


        //var userCart= await _cartService.ViewCartByUserId(userId);
             
           //var existingItems=userCart.Items;

        /*if (existingItems.Any())
        {
            
                    return Ok("Cart is Cleared Successfully..  ");

        }*/
        







    }




}
