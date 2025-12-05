using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SCleanArchitecture.SimpleAPI.Domain.Entities;

namespace CleanArchitecture.Infrastructure.Repositories;
//في هذه الطبقة لانقوم بعمل اي شروط او اي منطق فقط ننفذ العمليات ونضيف ونستخدم مصدر التخزين
//لا تضع شروط مثل: هل المنتج موجود؟ هل الكمية كافية؟ هل السلة مرتبطة بالمستخدم؟


internal sealed class WishlistRepository : IWishlistRepository //sealed
{

    private readonly ApplicationDbContext _context;//to deal and access the operations of DbContext Class

    public WishlistRepository(ApplicationDbContext context)
    {
        _context=context;


    }


    public async Task<Wishlist?> ViewWishlistByUserIdAsync(int userId)

    {

    //نقوم بجلب السلة مع العناصر التي بداخلها (اي عند جلب Wishlist ايضا تجلب معهWishlistItem)
     var userWishlist =await _context.Wishlists.Include(c=>c.Items)
        .ThenInclude(i => i.Product)//ثم تجلب المنتجات التي بداخل عناصر السلة
     .FirstOrDefaultAsync(c=>c.UserId==userId); 

    return userWishlist;//الان ترجع سلة واحده


/*Include()  
    -نابعه لمكتة EF Core
    
    يُخبر EF Core بأن يجلب الكيان المرتبط (هنا: c.Items) الذي من جدول <WishlistItem> مع الكيان الرئيسي (Wishlist) في نفس عملية الجلب.
وغالبا ما يترجم الى استعلام sql يستخدم join بين الجداول المرتبطة    

    باستخدام Include، نقول له: "عند جلب الكيان الرئيسي، اجلب أيضًا الكيان المرتبط به".
    اما بدونها سينم جلب السلة الكيان الرئيسي دون جلب الitmes فستكون السلة فارغة

    .FirstOrDefaultAsync(c => c.UserId == userId)

    يجلب أول سلة يكون UserId الخاص بها مساويًا لـ userId. إن لم توجد، تُرجع null. التنفيذ غير متزامن.
    من دونها سيتم ارجاع في include قائمة من Wishlists


    */


    //ThenInclude>> وهذا بما ان الجدول المرتبط بالسلة WishlistItem ايضا يرتبط به جدول الProduct




    }

    public async Task AddWishlistItemAsync(int userId, int productId)
    {

           
        
           var userProduct=new WishlistItem{

            WishlistId=userId,
            ProductId=productId,
           
           };
            
            //ثم نضيف المنتج الجديد الى Item والذي بدوره مرتبط ب قاعدة البيانات لجدول WishlistItems اي ستتم الاضافة ايضا في جدول قاعدة البيانات 

            _context.WishlistItems.Add(userProduct);

           await _context.SaveChangesAsync();

            Console.WriteLine($"{userProduct.WishlistId} - {userProduct.ProductId} ");//عند الادخال يتم طباعة ماتم اضافته لقاعدة البيانات

           // _context.Wishlists.Items.Add(userProduct);


        
        //await _context.SaveChangesAsync();//حفظ اي ما تم



    }


    public async Task RemoveWishlistItemAsync(int userId, int productId)
    {
        
        //1-نقوم بجلب السلة للتحقق من وجودها ام لا قبل الحذف
        var userWishlist=await ViewWishlistByUserIdAsync(userId);


       
        var existingProduct=userWishlist.Items.FirstOrDefault(i=>i.ProductId==productId);

            _context.WishlistItems.Remove(existingProduct);
        
            //userWishlist.Items.Remove(existingProduct);//بمجرد الحذف من القائمة التي في Wishlist ايضا يتم الحذف من جدول WishlistItem
            
           // _context.Items.Remove(existingProduct);
           await  _context.SaveChangesAsync();




    }

    public async Task ClearWishlistAsync(int userId)
    {
        //1-نقوم بجلب السلة للتحقق من وجودها ام لا قبل حذف كل مابداخلها
        var userWishlist=await ViewWishlistByUserIdAsync(userId);

        //2-ثانيا بعد التحقق من وجود السلة نقوم بالتحقق من وجود منتجات فيها
       // userWishlist.Items.Clear();

        var existingProducts=userWishlist.Items;

        
            
        _context.WishlistItems.RemoveRange(existingProducts);

           //userWishlist.Items.Clear();
           //userWishlist.Items.RemoveRange(existingProducts);
           // _context.WishlistItems.RemoveRange(existingProducts);
          await  _context.SaveChangesAsync();


    }

    public async Task AddWishlistAsync(Wishlist Wishlist)
    {
        _context.Wishlists.Add(Wishlist);
       await _context.SaveChangesAsync();

    }

    public async  Task SaveChangesAsync()
    {
        
       await _context.SaveChangesAsync();
    }

    
}
