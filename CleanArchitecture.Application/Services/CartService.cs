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


public interface ICartService
{
    //1-ViewCartByUserIdAsync
    Task<CartDto> ViewCartByUserId(int userId);//بمعنى قد ترجع كائن او null ?

    //2-AddCartItemAsync
    Task<AddCartItemResult> AddCartItem(int userId,int productId,int quantity);

    //3-RemoveCartItemAsync
    Task<bool> RemoveCartItem(int userId,int productId);

    //4-ClearCartAsync
    Task<ClearCartResult> ClearCart(int userId);

    decimal CalculateCartTotal(Cart cart);


}


internal class CartService : ICartService

{




    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;//؟

    //هنا قمنا بحقن اثنان من ال repos وهذا لاحتياجنا للوصول لعملية استعلام من كيان اخر 
    public CartService(ICartRepository cartRepository ,IProductRepository productRepository)
    {
        
        _cartRepository=cartRepository;
        _productRepository=productRepository;


    }
    public async Task<AddCartItemResult> AddCartItem(int userId, int productId, int quantity)


    {


        try
        {

              //اولا التحقق من اليوزر نفسه
       

         //1-للتحقق من وجود المنتج من جدول المنتجات وهل الكمية كافية قبل الاضافة بالسلة

            var product=await _productRepository.GetProductByIdAsync(productId);

            if(product ==null ||product.Stock <quantity)
            {
             return new AddCartItemResult
                {   
                    status=AddCartItemStatus.productNotFoundOrOutOfStock,
                };   


            }






        
         //نقوم بجلب السلة للتحقق من انها موجوده ام لا قبل الاضافة2-
        var userCart= await _cartRepository.ViewCartByUserIdAsync(userId);

        if(userCart==null)
        {
            //في حال كان لايوجد سلة للمستخدم نقوم بأنشاء سلة جديدة لمستخدم معين من خلال معرفه
            userCart=new Cart{UserId=userId};
            //ثم نضيف السلة الى قاعدة البيانات لجدول الCarts
            await _cartRepository.AddCartAsync(userCart);

        }


            var requestDto=new AddCartItemDto
        {
            
             ProductId=productId,
             Quantity=quantity,

        };
        var cartItemEntity=requestDto.ToCartItemEntity();

        
        
        
        
        //ثانيا اذا السلة موجود او تم انشاءها نتحقق من وجود نفس المنتج في السلة الذي نشتي نضيفه3-
        var  existingProduct=userCart.Items.FirstOrDefault(i=>i.ProductId==productId);

        if(existingProduct==null)
        {
            //في حال كان المنتج لايوجد مسبقا نقوم بأنشاءه او ادخال بياناته ثم اضافته

           /*var userProduct=new CartItem{
            CartId=userCart.Id,//ربط العنصر بالسلة الصحيحة
            ProductId=productId,
            Quantity=quantity,
           
           };*/

           //فيما قبل كنا نعمل userId وهذا خاطئ!
        await _cartRepository.AddCartItemAsync(userCart.Id,cartItemEntity.ProductId,cartItemEntity.Quantity);


            
            //ثم نضيف المنتج الجديد الى Item والذي بدوره مرتبط ب قاعدة البيانات لجدول CartItems اي ستتم الاضافة ايضا في جدول قاعدة البيانات 


            //userCart.Items.Add(userProduct);
            //await _cartRepository.SaveChangesAsync();
            //Console.WriteLine($"{userProduct.CartId} - {userProduct.ProductId} - {userProduct.Quantity}");//عند الادخال يتم طباعة ماتم اضافته لقاعدة البيانات

           // _context.Carts.Items.Add(userProduct);


        }else
        {
            //في حال كان المنتج موجود مسبقا يتم زيادة الكمية فقط بدل من اضافته من جديد

            existingProduct.Quantity +=quantity;
            Console.WriteLine("The quantity of this product is:"+$"{existingProduct.Quantity}");
            

        }

        await _cartRepository.SaveChangesAsync();//حفظ اي ما تم


           
            
        



        CartItem c=new CartItem
        {
            CartId=cartItemEntity.CartId,
            ProductId=cartItemEntity.ProductId,
            Quantity=cartItemEntity.Quantity,
            
        };
        var responeOfAddin=c.ToCartItemDto();

        return new AddCartItemResult
        {
            
             status=AddCartItemStatus.success,
             cartItemDto=responeOfAddin//هذا الي بيرجع في السواجر
        };





        //responeOfAddin


        }catch(Exception ex)
        {
             throw new ArgumentException($"Error in AddCartItem: {ex.Message}", ex);

        }

        
    }

    public decimal CalculateCartTotal(Cart cart)
    {

        


            //Sum >>هذه دالة LINQ في C#.تقوم بجمع كل القيم الناتجه من الشرط
            //أي أنها تأخذ كل ناتج من Quantity * Price لكل عنصر في السلة بالمرور على عنصر عنصر، ثم تجمعها معًا.اي تأخذ مضروب كل عنصر من ثم تجمعهم الكل
            /*
            Sum مع الـ Lambda هي في جوهرها مجرد حلقة تكرارية (Iteration) مثل 
            foreach، لكن مكتوبة بشكل مختصر وأنيق باستخدام LINQ.

            ايضا يمكن استخداد for loop لتحقيق  نفس الغرض

            */

            /*with foreach
            decimal total = 0;

foreach (var item in cart.Items)
{
    total += item.Quantity * item.Product.Price;
}



            */

            /* with fot loop
            decimal total = 0;

for (int index = 0; index < cart.Items.Count; index++)
{
    var item = cart.Items[index];
    total += item.Quantity * item.Product.Price;
}



            */



            /*var total=cart.Items.Sum(i=>i.Quantity * i.Product.Price);
            Console.WriteLine("The total of Cart is:");
            return total;*/

            

    if (cart?.Items == null || !cart.Items.Any())//اذا كانت السلة لاتحتوي على عناصر
        return 0;
//مالم
    // حساب المجموع مع التأكد أن المنتج موجود
    var total=cart.Items
               .Where(i => i != null && i.Product != null)//اذا كانت العناصر ليست فارغة و المنتجات ايضا ليست فارغة
               .Sum(i => i.Quantity * (decimal)i.Product.Price);

    return total; 





    }

    
    public async Task<ClearCartResult> ClearCart(int userId)
    {

        try
        { 

             //1-نقوم بجلب السلة للتحقق من وجودها ام لا قبل حذف كل مابداخلها
        var userCart=await _cartRepository.ViewCartByUserIdAsync(userId);

        if(userCart is null){


            Console.WriteLine("from Service layer:There is no Cart to Clear its products");

            //Console.WriteLine("The Cart is Empty , There is no products For deleting");
            return ClearCartResult.cartNotFound;
            
            }

        //2-ثانيا بعد التحقق من وجود السلة نقوم بالتحقق من وجود منتجات فيها
       // userCart.Items.Clear();

        var existingProducts=userCart.Items;

        if(!existingProducts.Any())
        {
            
           //userCart.Items.Clear();
            return ClearCartResult.cartEmpty;


           //userCart.Items.RemoveRange(existingProducts);
           // _context.CartItems.RemoveRange(existingProducts);
          //await  _context.SaveChangesAsync();
        }
        await _cartRepository.ClearCartAsync(userId);
        return ClearCartResult.cartCleared;



        }catch(Exception ex)
        {
             throw new ArgumentException($"Error in ClearCart: {ex.Message}", ex);

        }



    }

    public async Task<bool> RemoveCartItem(int userId, int productId)
    {

        try
        {


             //1-نقوم بجلب السلة للتحقق من وجودها ام لا قبل الحذف
        var userCart=await _cartRepository.ViewCartByUserIdAsync(userId);


        if(userCart is null)//اذا كان لايوجد سلة لايتم ارجاع شيء اي لايتم الحذف يتم الخروج
        { 
            Console.WriteLine("There is no Cart to Delete Items");

            return false;

        }
        //2-ثانيا اذا السلة موجود او تم انشاءها نتحقق من وجود نفس المنتج في السلة الذي نشتي نحذفه

        var existingProduct=userCart.Items.FirstOrDefault(i=>i.ProductId==productId);
        if(existingProduct is null)
        {

            
            return false;
        }

            await _cartRepository.RemoveCartItemAsync(userId,productId);
            //userCart.Items.Remove(existingProduct);//بمجرد الحذف من القائمة التي في Cart ايضا يتم الحذف من جدول CartItem
            return true;
           // _context.Items.Remove(existingProduct);
           //await  _context.SaveChangesAsync();




        }catch(Exception ex)
        {
            throw new ArgumentException($"Error in RemoveCartItem: {ex.Message}", ex);
 
        }


        
    }

    public async Task<CartDto> ViewCartByUserId(int userId)
    {

        try
        {
            //جلب سلة المستخدم من الريبو
        var userCart=await _cartRepository.ViewCartByUserIdAsync(userId);

        if(userCart is null)
        {
            Console.WriteLine("There is no Cart to view it");
            return null;

        }

        //تحويل بيانات السلة من Entity الى Dto
        var userCartDto = userCart.ToCartDto();//?



        // إذا السلة نفسها فارغة أو لا تحتوي عناصر

            foreach (var item in userCart.Items)//للتحقق من النتيجة هل ينم ارجاع المنتجات مع العناصر منقاعدة البيانات
{
    Console.WriteLine($"Item: {item.Product?.Name}, Qty: {item.Quantity}, Price: {item.Product?.Price}");
}    

        userCartDto.TotalPrice=CalculateCartTotal(userCart);




        //ارجاع قائمة الDTo مع المجموع
        return userCartDto;



        }catch(Exception ex)
        {
             throw new ArgumentException($"Error in ViewCartByUserId: {ex.Message}", ex);
   
        }
        



        


        

    }

}
