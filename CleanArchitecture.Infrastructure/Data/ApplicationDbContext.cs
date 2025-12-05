using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;//للتعامل مع قواعدة البيانات من حفظ وجلب وانواع جداول 
using SCleanArchitecture.SimpleAPI.Domain.Entities;

namespace CleanArchitecture.Infrastructure.Data;
/*
هذه المكتبه توفر
توفر الكلاس DbContext، 
والـ DbSet<T>، 
وطرق مثل SaveChangesAsync().
اي هي التي توفر عمليات ال 
CRUD

*/
//DbContext كلاس تابع لمكتبة EF Core
public class ApplicationDbContext : DbContext //هذا الكلاس هو النافذه او حلقة الوصل مابين تطبيقنا وقاعدة البيانات
{
	//ثانيا يتم تمرير اعدادات الاتصال الى الكلاس الاب
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{
		//بمعنى تمرير الاعدادات  الاتصال الى الكلاس الاب DbContext

		//ومن ثم AddApplicationDbContextبالاعتماد على  
		//this Constructor >> يستقبل إعدادات الاتصال ويُمررها للكلاس الأب
		// Sql Serverاخيرا يقوم الكلاس الاب بعد التمرير له بأستخدام هذه الاعدادات للتعامل مع 

	}

	//**هذا الكلاس فيه يتم الوصول لاعدادات الاتصال بقاعدة البيانات و تعريف الجداول 
	public DbSet<User> User { get; set; } = null!;//DbSet>> نوع بيانات تمثل جدول في قاعدة الببانات فيه بيانات من نوع المودل الذي في الدومين
	//Usersوسيكون اسم الجدول كما هو معرف هنا 


	//2-Product Table (that will be in Sql server)
	public DbSet<Product> Products{get; set;}=null!;


	//3-Cart Tables
	public DbSet<Cart> Carts{get; set;}=null;
	public DbSet<CartItem> CartItems{get; set;}=null;

	//4-Wishlist Tables

	public DbSet<Wishlist> Wishlists{get; set;}=null!;
	public DbSet<WishlistItem>WishlistItems{get; set;}=null!;


	//1-First we Create Navigation property in tables 
	//2-Configure Relationship between related tables here
	//3-finally ,we execute migration commands
	  protected override void OnModelCreating(ModelBuilder modelBuilder)//جميع العلاقات بين الكيانات يتم تعريفها بداخل هذه الدالة
    {//فيها يتم وضع العلاقات -المفاتيح-الفيود

        // علاقة واحد إلى متعدد بين Cart و CartItem
        modelBuilder.Entity<Cart>()
            .HasMany(c => c.Items)//from CartEntity
            .WithOne(i => i.Cart)
            .HasForeignKey(i => i.CartId);

		    // علاقة واحد إلى واحد بين User و Cart (إذا أردت سلة واحدة لكل مستخدم)	
		
		modelBuilder.Entity<User>()
		.HasOne(u => u.Cart)
		.WithOne(c => c.User)
		.HasForeignKey<Cart>(c =>c.UserId);



			/*
		المنتج الواحد (Product) يمكن أن يظهر في عدة عناصر سلة (CartItems) لمستخدمين مختلفين.

لكن كل عنصر سلة (CartItem) مرتبط بمنتج واحد فقط (Product).			
		*/

		    // علاقة واحد إلى متعدد بين Product و CartItem
		modelBuilder.Entity<Product>()
		.HasMany(p => p.CartItems)
		.WithOne(ci => ci.Product)
		.HasForeignKey(ci=> ci.ProductId);


		//علاقة واحد الى متعدد بين Wishlist ,WishlistItem

		modelBuilder.Entity<Wishlist>()
		.HasMany(w=>w.Items)
		.WithOne(wi=>wi.Wishlist)
		.HasForeignKey(wi=>wi.WishlistId);

		//علاقة واحد الى واحد بين User و Wishlist 

		modelBuilder.Entity<User>()
		.HasOne(u=>u.Wishlist)
		.WithOne(wi=>wi.User)
		.HasForeignKey<Wishlist>(wi=>wi.UserId);

		//علاقة واحد الى متعدد بين Product , Wishlist

		modelBuilder.Entity<Product>()
		.HasMany(p=>p.WishlistItems)
		.WithOne(wi=>wi.Product)
		.HasForeignKey(wi=>wi.ProductId);





    }

}

