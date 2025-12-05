using Microsoft.AspNetCore.Mvc;
using SCleanArchitecture.SimpleAPI.Application.DTOs;
using SCleanArchitecture.SimpleAPI.Application.Services;

namespace SCleanArchitecture.SimpleAPI.Controllers

{ //the class is expressed  as enclosing type

    [ApiController]
    [Route("products")]
     public class ProductController : ControllerBase
    {
        private readonly IProductService _ProductService;//تعريف متغير من نوع واجهة الخدمات 
        public ProductController(IProductService ProductService)//هنا يتم الحقن للواجهة ومن ثم تلقائيا يتم انشاء كائن من تنفيذ الواجهة من خلال ملف حقن الخدمات
        {
          _ProductService = ProductService;   
        }

        [HttpPost]   //هنا المستخدم يقوم بطلب تمرير بيانات من خلال ال controller
        public async Task<IActionResult> AddProduct(AddProductRequestDto ProductRequestDto)
        {
            /*var result = await _ProductService.AddProduct(ProductRequestDto);

            return Ok(result);*/


            var result = await _ProductService.AddProduct(ProductRequestDto);
            if(result == null)
            {
                return BadRequest("Invalid Product data.");
            }

            return Ok(result);
        }


        [HttpGet("all")] //GetAll Endpoint
        public async Task<IActionResult> GetAllProducts()
        
        {

            var result= await _ProductService.GetAllProducts();//تستدعي الدالة التابعه للservice والتي هي اساسا بدورها تستخدم دالة الريبو

            if(result is null){

                return NotFound("There is no Data to Get It!..");//لماذا لاتتنفذ
            }
            return Ok(result);


        }

        [HttpGet("{id}")]// GetById Endpoint             من اين اتى حقل الid

        public async Task<IActionResult>GetProductById(int id){
        
        var result= await _ProductService.GetProductById(id);//تستدعي الدالة التابعه للservice والتي هي اساسا بدورها تستخدم دالة الريبو

        if(result == null){
            return NotFound("Product not found");//اين تعرض؟

        }

        return Ok(result);


        }


    [HttpDelete("{id}")] //Delete Endpoint 

    public async Task<IActionResult>DeleteProductById(int id )
    {
          var deleted=  await _ProductService.DeleteProductById(id);//القيمه المخزنه هنا true or false

            if (!deleted)
                return NotFound("Product is't deleted,Product not found..!");//الرساله التي تنعرض في السواجر في حال لم يوجد مستخدم يمكن حذفه

            return Ok("Product deleted successfully");//في حال كان المستخدم موجود وتم الحذف


    }

    /*[HttpDelete("all")] //DeleteAll Endpoint 

    public async Task<IActionResult>DeleteAllUesrs()
    {
          var Notdeleted=  await _ProductService.DeleteAllUesrs();//القيمه المخزنه هنا true or false

            if (Notdeleted)
                {return NotFound("Products is't deleted,All of Products are not found..!");}//الرساله التي تنعرض في السواجر في حال لم يوجد مستخدمين يمكن حذفهم

            return Ok("All of Products deleted successfully");//في حال كان المستخدمين موجود وتم الحذف


    }
    */

    [HttpPut("{id}")] 

    public async Task<IActionResult>UpdateProduct(int id,ProductDto updatedProductDto)
    {
            //هنا الشروط هذه من اجل عرض رسائل في السواجر فقط
            if(id != updatedProductDto.Id){//يتحقق من أن رقم المستخدم المدخل في الرابط اي السواجر يطابق الرقم الموجود داخل الجسم (body)،

            return BadRequest("Product ID mismatch!!!!.");//BadRequest,NotFound هذه فقط عناوين استجابة لتوضيح الخطأ وما بداخلها هو الرسالة الوصف

            }
            var updated=await _ProductService.UpdateProduct(updatedProductDto);

            if(!updated){
             return NotFound("Product is't updated ,Product not found..!");//الرساله التي تنعرض في السواجر في حال لم يوجد مستخدم


            }
            return Ok("Product Updated Successfully>>");//الرساله في السواجر في حال نجحت العمليه




    }







    }









}


/*
كيف يتم التعرف على الخدمات من طبقة الServices
-عندما يأتي طلب HTTP إلى ProductController
-ASP.NET Core يرى أن الـ Constructor يحتاج IProductService.
-يبحث في الـ DI: هل مسجل عندي IProductService؟
-يجد أنه مسجل مع ProductService.
-ينشئ نسخة او كائن من ProductService ويعطيها للـ Controller.









*/