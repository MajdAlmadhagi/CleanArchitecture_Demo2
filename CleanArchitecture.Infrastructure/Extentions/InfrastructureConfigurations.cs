using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SCleanArchitecture.SimpleAPI.Infrastructure.Repositories;
using System;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SCleanArchitecture.SimpleAPI.Infrastructure.Extentions;


 //Repositoriesملف تجميع ال
    /*
     * هذا فقط ينظّم الكود.
بدلاً من تسجيل كل 
     * Repository 
     * يدويًا،
نضعها كلها في مكان واحد لتُسجَّل بسطر واحد لاحقًا.
     * 
     * 
     * 
     * 
     * 
     * 
     */


/*
هو ملف امتداد (Extension Method) 
لتجميع كل خدمات البنية التحتية في مكان واحد، ويتم استدعاؤه من 
Program.cs.
*/
public static class InfrastructureConfigurations
{
    //AddInfrastructureServi🤬ces للتجميع
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddUserRepsitory();
        services.AddProductRepository();
        services.AddCartRepository();
        services.AddWishlistRepository();
        services.AddApplicationDbContext();//from down

        return services;
    }



    /*
    Dependency injection

    AddApplicationDbContext..

    تُستخدم لإضافة خدمة 
    ApplicationDbContext 
    إلى حاوية الاعتمادية 
    (Dependency Injection Container). 
    بمعنى يتم حقنها الى كلاس
    IServiceCollection
    التابع لمايكروسوفت والذي يقوم بتفعيل  الكلاس في !!

    */


    //اولا هنا يتم تجهيز اعدادات الاتصال
    private static IServiceCollection AddApplicationDbContext(this IServiceCollection services)
    {//هي دالة تضيف ApplicationDbContext وتربطه بـ SQL Server

        //1-نص الاتصال  بقاعدة البيانات
        string dbConnection = "Data Source =.; Database = train; Integrated Security = True; Connect Timeout = 30; Encrypt = True; Trust Server Certificate = True; Application Intent = ReadWrite; Multi Subnet Failover = False";//HospitalConstants.HospitalDbConnection;//Environment.GetEnvironmentVariable(HospitalConstants.HospitalDb);
        //Data Source اسم السيرفر 
        //Database اسم قاعدة البيانات
        //Security المصادقة
        //إعدادات إضافية مثل التشفير والمهلة. 


        //2-
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(dbConnection));


        //AddDbContext<T>()
        //  هي دالة تابعه 
        // EF Core
        //  تضيف الكلاس  او تحقنه إلى الخدمات التابعه لمايكروسوفت
        //وهي مثل ..AddScopoed()




        /*
        هذا السطر يربط 
        EF Core 
        اي يربط عمليات المشروع
         بقاعدة بيانات 
         SQL Server
          باستخدام سلسلة الاتصال.
        */

        return services;
    }
}