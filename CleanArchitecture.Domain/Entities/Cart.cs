using SCleanArchitecture.SimpleAPI.Domain.Entities;

namespace CleanArchitecture.Domain.Entities;

public class Cart
{

    public int Id {get; set;} //CartId
    public int UserId{get; set;}//F.K to User Entity

    // 1-Navigation Property for realtion(لتوضيح ان هذا الجدول له علاقه مع جدول العناصر)
    public List<CartItem> Items{get; set;}=new List<CartItem>();//السلة الواحدة لها اكثر من عنصر او منتج
     //Items its Navigation Property of CartItem that means it depends of CartItem



    //Navigation property (علاقة السلة بجدول User)
    public User User{get; set;}//السلة الواحدة تكون لمستخدم واحد فقط

}

//يعني: الإضافة او غيرها من عمليات على الـ Navigation Property = إضافة او قراءة سجل جديد في جدول CartItem.
/*
    //EF core تتعرف تلقائيا على العلاقة من خلال Navigation property
    //  عن طريق include وبالتالي اي عملية تتم على الproperty تتم ايضا على الجدول
    المرتبطه به سواء من اضافة او جلب


*/
 //one-to -many >>السلة يكون لها اكثر من عنصر والعنصر يكون مرتبط بسلة واحدة
public class CartItem
{
    public int Id {get; set;} //ItemId

    public int CartId {get; set;} //F.K to Cart Entity becuase those items belong to Cart

    public int ProductId {get; set;} //F.k to Product Entity 
    public int Quantity {get; set;} //Quantity of this Item


        //Navigation Property for realtion(لتوضيح ان هذا الجدول له علاقة مع جدول السلة)
        
    public Cart Cart{get; set;}//هنا ليس قائمة وهذا يعني ان العناصر ترتبط بسلة واحدة فقط


    //Navigation property(توضح ان هذا الجدول له علاقة بجدول Products)
    //one-to-one
    public Product Product{get; set;}//عنصر السلة الواحد يحتوي على منتج واحد

}
