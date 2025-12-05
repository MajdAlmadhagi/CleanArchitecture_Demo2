using CleanArchitecture.Application.Converters;
using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using SCleanArchitecture.SimpleAPI.Application.Enums;
using SCleanArchitecture.SimpleAPI.Domain.Entities;
using SCleanArchitecture.SimpleAPI.Domain.Repositories;

namespace CleanArchitecture.Application.Services;

//في هذه الطبقه فقط نضيف الشروط والمنطق

// تضع شروط مثل: هل المنتج موجود؟ هل الكمية كافية؟ هل السلة مرتبطة بالمستخدم؟


public interface IWishlistService
{
    //1-ViewWishlistByUserIdAsync
    Task<WishlistDto> ViewWishlistByUserId(int userId);//بمعنى قد ترجع كائن او null ?

    //2-AddWishlistItemAsync
    Task<AddWishlistItemResult> AddWishlistItem(int userId,int productId);

    //3-RemoveWishlistItemAsync
    Task<bool> RemoveWishlistItem(int userId,int productId);

    //4-ClearWishlistAsync
    Task<ClearWishlistResult> ClearWishlist(int userId);

}


internal class WishlistService : IWishlistService

{




    private readonly IWishlistRepository _WishlistRepository;
    private readonly IProductRepository _productRepository;//؟

    //هنا قمنا بحقن اثنان من ال repos وهذا لاحتياجنا للوصول لعملية استعلام من كيان اخر 
    public WishlistService(IWishlistRepository WishlistRepository ,IProductRepository productRepository)
    {
        
        _WishlistRepository=WishlistRepository;
        _productRepository=productRepository;


    }
    public async Task<AddWishlistItemResult> AddWishlistItem(int userId, int productId)


    {


        try
        {


            //1-للتحقق من وجود المنتج من جدول المنتجات وهل الكمية كافية قبل الاضافة بالسلة

            var product=await _productRepository.GetProductByIdAsync(productId);

            if(product ==null )
            {
             return new AddWishlistItemResult
             {
                 status=AddWishlistItemStatus.productNotFoundOrOutOfStock,
                };   


            }






       
        
         //نقوم بجلب السلة للتحقق من انها موجوده ام لا قبل الاضافة2-
        var userWishlist= await _WishlistRepository.ViewWishlistByUserIdAsync(userId);

        if(userWishlist==null)
        {
            //في حال كان لايوجد سلة للمستخدم نقوم بأنشاء سلة جديدة لمستخدم معين من خلال معرفه
            userWishlist=new Wishlist{
                UserId=userId,


                };
            //ثم نضيف قائمة المفضلة الى قاعدة البيانات لجدول الWishlists
            await _WishlistRepository.AddWishlistAsync(userWishlist);
            await _WishlistRepository.SaveChangesAsync();

        }


            var requestDto=new AddWishlistItemDto
        {
             ProductId=productId,
            // Quantity=quantity,

        };
        var WishlistItemEntity=requestDto.ToWishlistItemEntity();

        
        
        
        
        //ثانيا اذا القائمة موجوده او تم انشاءها نتحقق من وجود نفس المنتج في السلة الذي نشتي نضيفه3-
        var  existingProduct=userWishlist.Items.FirstOrDefault(i=>i.ProductId==productId);

        if(existingProduct==null)
        {
            //في حال كان المنتج لايوجد مسبقا نقوم بأنشاءه او ادخال بياناته ثم اضافته

           /*var userProduct=new WishlistItem{
            WishlistId=userWishlist.Id,//ربط العنصر بالسلة الصحيحة
            ProductId=productId,
            Quantity=quantity,
           
           };*/
        await _WishlistRepository.AddWishlistItemAsync(userWishlist.Id,WishlistItemEntity.ProductId);



                //ثم نضيف المنتج الجديد الى Item والذي بدوره مرتبط ب قاعدة البيانات لجدول WishlistItems اي ستتم الاضافة ايضا في جدول قاعدة البيانات 


                //userWishlist.Items.Add(userProduct);
                //await _WishlistRepository.SaveChangesAsync();
                //Console.WriteLine($"{userProduct.WishlistId} - {userProduct.ProductId} - {userProduct.Quantity}");//عند الادخال يتم طباعة ماتم اضافته لقاعدة البيانات

                // _context.Wishlists.Items.Add(userProduct);


            }
            else //اذا كان المنتج موجود مسبقا في قائمة المفضلات لاتعمل شيء
            {
                 Console.WriteLine("*********The Product is already exist in wishlist.!!********");

                return new AddWishlistItemResult
                {
                    status=AddWishlistItemStatus.ProductAlreadyExist,

                };
            

            }

        await _WishlistRepository.SaveChangesAsync();//حفظ اي ما تم


           
            
        



        WishlistItem c=new WishlistItem
        {
            WishlistId=WishlistItemEntity.WishlistId,
            ProductId=WishlistItemEntity.ProductId,
            //Quantity=WishlistItemEntity.Quantity,
            
        };
        var responeOfAddin=c.ToWishlistItemDto();

        return new AddWishlistItemResult
        {
            
             status=AddWishlistItemStatus.success,
             WishlistItemDto=responeOfAddin//هذا الي بيرجع في السواجر في حال لم تتحقق جميع الشروط السابقه
        };  





        //responeOfAddin


        }catch(Exception ex)
        {
             throw new ArgumentException($"Error in AddWishlistItem: {ex.Message}", ex);

        }

        
    }


    public async Task<ClearWishlistResult> ClearWishlist(int userId)
    {

        try
        { 

             //1-نقوم بجلب السلة للتحقق من وجودها ام لا قبل حذف كل مابداخلها
        var userWishlist=await _WishlistRepository.ViewWishlistByUserIdAsync(userId);

        if(userWishlist is null){


            Console.WriteLine("from Service layer:There is no Wishlist to Clear its products");

            //Console.WriteLine("The Wishlist is Empty , There is no products For deleting");
            return ClearWishlistResult.WishlistNotFound;
            
            }

        //2-ثانيا بعد التحقق من وجود السلة نقوم بالتحقق من وجود منتجات فيها
       // userWishlist.Items.Clear();

        var existingProducts=userWishlist.Items;

        if(!existingProducts.Any())
        {
            
           //userWishlist.Items.Clear();
            return ClearWishlistResult.WishlistEmpty;


           //userWishlist.Items.RemoveRange(existingProducts);
           // _context.WishlistItems.RemoveRange(existingProducts);
          //await  _context.SaveChangesAsync();
        }
        await _WishlistRepository.ClearWishlistAsync(userId);
        return ClearWishlistResult.WishlistCleared;



        }catch(Exception ex)
        {
             throw new ArgumentException($"Error in ClearWishlistItem: {ex.Message}", ex);

        }



    }

    public async Task<bool> RemoveWishlistItem(int userId, int productId)
    {

        try
        {


             //1-نقوم بجلب السلة للتحقق من وجودها ام لا قبل الحذف
        var userWishlist=await _WishlistRepository.ViewWishlistByUserIdAsync(userId);


        if(userWishlist is null)//اذا كان لايوجد سلة لايتم ارجاع شيء اي لايتم الحذف يتم الخروج
        { 
            Console.WriteLine("There is no Wishlist to Delete Items");

            return false;

        }
        //2-ثانيا اذا السلة موجود او تم انشاءها نتحقق من وجود نفس المنتج في السلة الذي نشتي نحذفه

        var existingProduct=userWishlist.Items.FirstOrDefault(i=>i.ProductId==productId);
        if(existingProduct is null)
        {

            
            return false;
        }

            await _WishlistRepository.RemoveWishlistItemAsync(userId,productId);
            //userWishlist.Items.Remove(existingProduct);//بمجرد الحذف من القائمة التي في Wishlist ايضا يتم الحذف من جدول WishlistItem
            return true;
           // _context.Items.Remove(existingProduct);
           //await  _context.SaveChangesAsync();




        }catch(Exception ex)
        {
            throw new ArgumentException($"Error in RemoveWishlistItem: {ex.Message}", ex);
 
        }


        
    }

    public async Task<WishlistDto> ViewWishlistByUserId(int userId)
    {

        try
        {
            //جلب سلة المستخدم من الريبو
        var userWishlist=await _WishlistRepository.ViewWishlistByUserIdAsync(userId);

        if(userWishlist is null)
        {
            Console.WriteLine("There is no Wishlist to view it");
            return null;

        }

        //تحويل بيانات السلة من Entity الى Dto
        var userWishlistDto = userWishlist.ToWishlistDto();//?



        // إذا السلة نفسها فارغة أو لا تحتوي عناصر

            foreach (var item in userWishlist.Items)//للتحقق من النتيجة هل ينم ارجاع المنتجات مع العناصر منقاعدة البيانات
{
    Console.WriteLine($"Item: {item.Product?.Name}");
}    




        //ارجاع قائمة الDTo مع المجموع
        return userWishlistDto;



        }catch(Exception ex)
        {
             throw new ArgumentException($"Error in ViewWishlistItem: {ex.Message}", ex);
   
        }
        



        


        

    }

}
