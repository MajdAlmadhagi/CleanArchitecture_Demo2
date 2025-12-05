using CleanArchitecture.Application.Services;
using Microsoft.AspNetCore.Mvc;
using SCleanArchitecture.SimpleAPI.Application.Enums;

namespace SCleanArchitecture.SimpleAPI.Controllers;

[ApiController]
[Route("Wishlist")]
public class WishlistController : ControllerBase
{
    private readonly IWishlistService _WishlistService;
    public WishlistController(IWishlistService WishlistService)
    {
        _WishlistService=WishlistService;
    }


    [HttpPost]

    public async Task<ActionResult> AddWishlistItem(int userId,int productId)
    {
        var result= await _WishlistService.AddWishlistItem(userId,productId);


        if (result.status == AddWishlistItemStatus.ProductAlreadyExist)
        {
            return BadRequest("Sorry, this Product is Already exist.!!");
        }

        if (result.status == AddWishlistItemStatus.productNotFoundOrOutOfStock)
        {
            return BadRequest("Sorry, this Product is not found.");
        }
        

        if(result.status == AddWishlistItemStatus.success)
        {
            
            return Ok(result.WishlistItemDto);
        }

        return StatusCode(500, "Unexpected error.");

        /*if(result == null)
        {
            return BadRequest("Invalid WishlistItem Data");
        }*/

        //return Ok(result);
    }


    [HttpGet]
    public async Task<ActionResult> ViewWishlistByUserId(int userId)
    {
        var result= await _WishlistService.ViewWishlistByUserId(userId);

        if(result == null)
        {
            return NotFound("Wishlist Not Found!!");
        }

        return Ok(result);
    }

    [HttpDelete]

    public async Task<ActionResult> RemoveWishlistItem(int userId, int productId)
    {
         var result= await _WishlistService.ViewWishlistByUserId(userId);

           var deleted=await _WishlistService.RemoveWishlistItem(userId,productId);

        if (!deleted)
        {
         return  NotFound("Product is't removed from Wishlist,Product not found..!"); 

        }
        return Ok("Product removed successfully");//في حال كان المستخدم موجود وتم الحذف
 

    }

    [HttpDelete("Clear")]

    public async Task<ActionResult> ClearWishlist(int userId)
    {
        
        var result=await _WishlistService.ClearWishlist(userId);

        if (result==ClearWishlistResult.WishlistNotFound)
        {
            return NotFound("There is no Wishlist to Clear its products ");
        }

        if (result == ClearWishlistResult.WishlistEmpty)
        {
            return BadRequest("This Wishlist is Empty,no items to clear it");
        }

        if (result == ClearWishlistResult.WishlistCleared)
        {
         return Ok("Wishlist is Cleared Successfully..");

        }

            return StatusCode(500, "Unexpected error.");


    }




}
