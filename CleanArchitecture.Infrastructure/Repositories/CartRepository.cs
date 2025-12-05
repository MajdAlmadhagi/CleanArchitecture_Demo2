using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SCleanArchitecture.SimpleAPI.Domain.Entities;

namespace CleanArchitecture.Infrastructure.Repositories;
//في هذه الطبقة لانقوم بعمل اي شروط او اي منطق فقط ننفذ العمليات ونضيف ونستخدم مصدر التخزين
//لا تضع شروط مثل: هل المنتج موجود؟ هل الكمية كافية؟ هل السلة مرتبطة بالمستخدم؟


internal sealed class CartRepository : ICartRepository //sealed
{

    private readonly ApplicationDbContext _context;//to deal and access the operations of DbContext Class

    public CartRepository(ApplicationDbContext context)
    {
        _context=context;


    }


    public async Task<Cart?> ViewCartByUserIdAsync(int userId)

    {

    //نقوم بجلب السلة مع العناصر التي بداخلها (اي عند جلب Cart ايضا تجلب معهCartItem)
     var userCart =await _context.Carts.Include(c=>c.Items)
        .ThenInclude(i => i.Product)//ثم تجلب المنتجات التي بداخل عناصر السلة
     .FirstOrDefaultAsync(c=>c.UserId==userId); 

    return userCart;//الان ترجع سلة واحده


/*Include()  
    -نابعه لمكتة EF Core
    
    يُخبر EF Core بأن يجلب الكيان المرتبط (هنا: c.Items) الذي من جدول <CartItem> مع الكيان الرئيسي (Cart) في نفس عملية الجلب.
وغالبا ما يترجم الى استعلام sql يستخدم join بين الجداول المرتبطة    

    باستخدام Include، نقول له: "عند جلب الكيان الرئيسي، اجلب أيضًا الكيان المرتبط به".
    اما بدونها سينم جلب السلة الكيان الرئيسي دون جلب الitmes فستكون السلة فارغة

    .FirstOrDefaultAsync(c => c.UserId == userId)

    يجلب أول سلة يكون UserId الخاص بها مساويًا لـ userId. إن لم توجد، تُرجع null. التنفيذ غير متزامن.
    من دونها سيتم ارجاع في include قائمة من Carts


    */


    //ThenInclude>> وهذا بما ان الجدول المرتبط بالسلة CartItem ايضا يرتبط به جدول الProduct




    }

    public async Task AddCartItemAsync(int userId, int productId, int quantity)
    {

           
        
           var userProduct=new CartItem{

            CartId=userId,
            ProductId=productId,
            Quantity=quantity,
           
           };
            
            //ثم نضيف المنتج الجديد الى Item والذي بدوره مرتبط ب قاعدة البيانات لجدول CartItems اي ستتم الاضافة ايضا في جدول قاعدة البيانات 

            _context.CartItems.Add(userProduct);

           await _context.SaveChangesAsync();

            Console.WriteLine($"{userProduct.CartId} - {userProduct.ProductId} - {userProduct.Quantity}");//عند الادخال يتم طباعة ماتم اضافته لقاعدة البيانات

           // _context.Carts.Items.Add(userProduct);


        
        //await _context.SaveChangesAsync();//حفظ اي ما تم



    }


    public async Task RemoveCartItemAsync(int userId, int productId)
    {
        
        //1-نقوم بجلب السلة للتحقق من وجودها ام لا قبل الحذف
        var userCart=await ViewCartByUserIdAsync(userId);


       
        var existingProduct=userCart.Items.FirstOrDefault(i=>i.ProductId==productId);

            _context.CartItems.Remove(existingProduct);
        
            //userCart.Items.Remove(existingProduct);//بمجرد الحذف من القائمة التي في Cart ايضا يتم الحذف من جدول CartItem
            
           // _context.Items.Remove(existingProduct);
           await  _context.SaveChangesAsync();




    }

    public async Task ClearCartAsync(int userId)
    {
        //1-نقوم بجلب السلة للتحقق من وجودها ام لا قبل حذف كل مابداخلها
        var userCart=await ViewCartByUserIdAsync(userId);

        //2-ثانيا بعد التحقق من وجود السلة نقوم بالتحقق من وجود منتجات فيها
       // userCart.Items.Clear();

        var existingProducts=userCart.Items;

        
            
        _context.CartItems.RemoveRange(existingProducts);

           //userCart.Items.Clear();
           //userCart.Items.RemoveRange(existingProducts);
           // _context.CartItems.RemoveRange(existingProducts);
          await  _context.SaveChangesAsync();


    }

    public async Task AddCartAsync(Cart cart)
    {
        _context.Carts.Add(cart);
       await _context.SaveChangesAsync();

    }

    public async  Task SaveChangesAsync()
    {
        
       await _context.SaveChangesAsync();
    }

}
