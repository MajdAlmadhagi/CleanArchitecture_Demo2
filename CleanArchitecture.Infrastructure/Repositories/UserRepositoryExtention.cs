using Microsoft.Extensions.DependencyInjection;
using SCleanArchitecture.SimpleAPI.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace SCleanArchitecture.SimpleAPI.Infrastructure.Repositories;

internal static class UserRepositoryExtention
{
    public static IServiceCollection AddUserRepsitory(this IServiceCollection services)

        /*يعرّف النظام كيف ينشئ كائنًا من 
         *  
         * UserRepository
         * في ملف ال
         * Services
         * 
         * 
         */

    {
        services.AddScoped<IUserRepository, UserRepository>();
        //يُنشئ كائن جديد لكل طلب HTTP واحد فقط
        //(الأنسب في Web APIs ✅)

        return services;
    }


    //??  هل استطيع وضع الحقن للجدول التالي 
}


    /*
    يا نظام، كلما رأيت أحد يحتاج IUserRepository،
أعطه كائن (object) من UserRepository.”

    */


    /*
     * 
     * عندما يحتاج UserService إلى IUserRepository،
الـ .NET Dependency Injection Container يبحث في التسجيلات،
ويجد أنك سجلت العلاقة التالية:

IUserRepository  ←  UserRepository


فيقوم النظام بعمل شيء مثل:

_userRepository = new UserRepository();
     * 
     * 
     * 
     * 
     * 
    */