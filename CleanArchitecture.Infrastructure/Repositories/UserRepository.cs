using CleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SCleanArchitecture.SimpleAPI.Domain.Entities;
using SCleanArchitecture.SimpleAPI.Domain.Repositories;


namespace SCleanArchitecture.SimpleAPI.Infrastructure.Repositories;

//هذا الملف يستخدم ملف الداتا من اجل تنفيذ مايقدمه الكلاس تبعه
//يعتمد على ApplicationDbContext  من Data.

//يستخدم ApplicationDbContext لتنفيذ CRUD


/*
هو تنفيذ واجهة IUserRepository
، وينفذ عمليات 
CRUD 
على جدول 
Users.
*/


//ثالثا واخيرا يتم استخدام عمليات الكلاس الاب من الكلاس الوارث
//DIرابعا يستدعي الخدمات  عبر
internal sealed class UserRepository : IUserRepository
{
       // private static readonly  List<User> userList = new(); 


    private readonly ApplicationDbContext _context;//to deal and access the operations of DbContext Class
    public UserRepository(ApplicationDbContext context)//?
    {
        _context = context;
    }

    public async Task AddUserAsync(User user)
    {
         //userList.Add(user);  // add new user to the table 

        //await Task.CompletedTask;


        _context.User.Add(user);  // add new user to the table Users that exists in  ApplicationDbContext
        await _context.SaveChangesAsync();  // Save the changes in DB ,those functions from data file
        Console.WriteLine($"{user.Id} - {user.Name} - {user.Email}");//عند الادخال يتم طباعة ماتم اضافته لقاعدة البيانات
    }

    public async Task<List<User>> GetAllUsersAsync ()
    {
        
        /*
        في C# 
        عندما نكتب دالة 
        async (أي دالة غير متزامنة)،
يجب أن تُعيد Task أو Task<T>.


        */
        
    //return await Task.FromResult(userList);//تعيد النتيجه بشكل غير متزامن

    var DbUsers=await _context.User.ToListAsync();
    return DbUsers;
   // return  Task.FromResult(DbUsers);//تعيد النتيجه بشكل غير متزامن





        /*

        return _users; ← تُعيد القائمة مباشرة (عادي).

return Task.FromResult(_users); ← تُعيدها كمهمة Task (غير متزامنة).

await Task.FromResult(_users); ← تنتظر النتيجة من الـ Task، ثم تُعيدها.

        */
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        /*var user = userList.FirstOrDefault(u => u.Id == id);
            return await Task.FromResult(user);*/


            var user=await _context.User.FindAsync(id);
            return user;




            /*

            هذه دالة LINQ جاهزة من .NET 
            تُستخدم للبحث داخل المجموعات.

        تعني:

    "أعطني أول عنصر في القائمة يحقق الشرط الذي سأضعه بين القوسين (...).
    وإذا لم أجد أي عنصر يحقق الشرط، أرجع القيمة الافتراضية 
    (Default)."
    والتي ستكون 
    null


            */
    }


    public async Task DeleteUserAsync(int id)
    {
        //var user = userList.FirstOrDefault(u => u.Id == id);
       // userList.Remove(user)
                    //await Task.CompletedTask;
         //Task.CompletedTask; عملناها هكذا لانه لاترجع نوع او قيمه


             var user=await _context.User.FindAsync(id);//اولا نأخذ اليوزر ذات المعرف المعين
            // user الان هذا المتغير يحتوي على قيمة وهي كائن بأكمله
           // if (user is not null){
              _context.User.Remove(user);//هنا يتم حذف اليوزر الذي تم جلبه في السطر السابق
               await _context.SaveChangesAsync();//لحفظ التغيير والذي هو الحذف في قاعدة البيانات
            //}
            //await with SaveChangesAsync because it depends on asyncronization
            //Remove() >>for one object just

    }

    


    public async Task UpdateUserAsync(User user)
    {

       //var existingUser=userList.FirstOrDefault(u=> u.Id==user.Id);//user.Id      هذه البيانات التي تم تخزينها ك   UserEntity وبالتالي في هذه الطبقه يتم التعامل مع الدومين فقط 
        var existingUser=await _context.User.FindAsync(user.Id);//اولا نأخذ اليوزر ذات المعرف المعين

        //if(existingUser is not null){

            /*
            تحديث البيانات القادمه من المستخدم ك
            Dto 
            من ثم تصبح هنا ك
            Entity 
            لان التعامل هنا مع الدومين

            اي بدون التحويل كان هكذا
            existingUser.Name=userDto.Name,

            */
            existingUser.Name=user.Name;
            existingUser.Email=user.Email;

            _context.User.Update(existingUser);
           await _context.SaveChangesAsync();

                Console.WriteLine("*****Users After Update:");
                   Console.WriteLine($"{existingUser.Name} - {existingUser.Email}");//عند الادخال يتم طباعة ماتم اضافته لقاعدة البيانات




        }

       // await Task.CompletedTask;//لانها لاترجع قيمه


   // }


    public async Task DeleteAllUesrsAsync()
    {
        //اولا نجلب كل المستخدمين الذي نريد حذفهم
         var users= await _context.User.ToListAsync();

         //if(users is not null){

            _context.User.RemoveRange(users);
           await _context.SaveChangesAsync();

        // }
        //RemoveRange for List of objects

        
    }



}


