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
internal class ApplicationDbContext : DbContext //هذا الكلاس هو النافذه او حلقة الوصل مابين تطبيقنا وقاعدة البيانات
{
	//ثانيا يتم تمرير اعدادات الاتصال الى الكلاس الاب
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)//بمعنى تمرير الاعدادات  الاتصال الى الكلاس الاب DbContext
	{
		//ومن ثم AddApplicationDbContextبالاعتماد على  
		//this Constructor >> يستقبل إعدادات الاتصال ويُمررها للكلاس الأب
		// Sql Serverاخيرا يقوم الكلاس الاب بعد التمرير له بأستخدام هذه الاعدادات للتعامل مع 

	}

	//هذا الكلاس فيه يتم الوصول لاعدادات الاتصال بقاعدة البيانات و تعريف الجداول 
	public DbSet<User> Users { get; set; } = null!;//DbSet>> نوع بيانات تمثل جدول في قاعدة الببانات فيه بيانات من نوع المودل الذي في الدومين
	//Usersوسيكون اسم الجدول كما هو معرف هنا 


}

