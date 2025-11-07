using CleanArchitecture.Infrastructure.Data;
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
    private readonly ApplicationDbContext _context;//to deal and access the operations of DbContext Class
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddUserAsync(User user)
    {
        _context.Users.Add(user);  // add new user to the table 
        await _context.SaveChangesAsync();  // add new user to the table ,this function from data file
    }

    public Task DeleteUserAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAllUsersAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);

        return user;
    }

    public Task UpdateUserAsync(User user)
    {
        throw new NotImplementedException();
    }
}


