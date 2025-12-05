using SCleanArchitecture.SimpleAPI.Domain.Entities;

namespace CleanArchitecture.Domain.Entities;

public class Wishlist
{

    public int Id {get; set;} //WishlistId
    public int UserId{get; set;}//F.K to User Entity

    // 1-Navigation Property for realtion(لتوضيح ان هذا الجدول له علاقه مع جدول العناصر)
    public List<WishlistItem> Items{get; set;}=new List<WishlistItem>();//المفضلة الواحدة لها اكثر من عنصر او منتج
     //Items its Navigation Property of WishlistItem that means it depends of WishlistItem



    //Navigation property (علاقة جدول المفضلة بجدول User)
    public User User{get; set;}//المفضلة الواحدة تكون لمستخدم واحد فقط

}

//يعني: الإضافة او غيرها من عمليات على الـ Navigation Property = إضافة او قراءة سجل جديد في جدول WishlistItem.
/*
    //EF core تتعرف تلقائيا على العلاقة من خلال Navigation property
    //  عن طريق include وبالتالي اي عملية تتم على الproperty تتم ايضا على الجدول
    المرتبطه به سواء من اضافة او جلب


*/
 //one-to -many >>المفضلة يكون لها اكثر من عنصر والعنصر او المنتج يكون مرتبط بمفضلة واحدة
public class WishlistItem
{
    public int Id {get; set;} //ItemId

    public int WishlistId {get; set;} //F.K to Wishlist Entity becuase those items belong to Wishlist

    public int ProductId {get; set;} //F.k to Product Entity 
     //هنا لانحتاج للكميه



        //Navigation Property for realtion(لتوضيح ان هذا الجدول له علاقة مع جدول السلة)
    public Wishlist Wishlist{get; set;}//هنا ليس قائمة وهذا يعني ان العناصر ترتبط بمفضلة واحدة فقط


    //Navigation property(توضح ان هذا الجدول له علاقة بجدول Products)
    //one-to-one
    public Product Product{get; set;}//عنصر المفضلة الواحد يحتوي على منتج واحد

}
