using Microsoft.AspNetCore.Mvc;
using SCleanArchitecture.SimpleAPI.Application.DTOs;
using SCleanArchitecture.SimpleAPI.Application.Services;

namespace SCleanArchitecture.SimpleAPI.Controllers
{
    [ApiController]
    [Route("users")]
     public class UserController : ControllerBase
    {
        private readonly IUserService _userService;//تعريف متغير من نوع واجهة الخدمات 
        public UserController(IUserService userService)//هنا يتم الحقن للواجهة ومن ثم تلقائيا يتم انشاء كائن من تنفيذ الواجهة من خلال ملف حقن الخدمات
        {
          _userService = userService;   
        }

        [HttpPost]   //هنا المستخدم يقوم بطلب تمرير بيانات من خلال ال controller
        public async Task<IActionResult> AddUser(AddUserRequestDto userRequestDto)
        {
            /*var result = await _userService.AddUser(userRequestDto);

            return Ok(result);*/


            var result = await _userService.AddUser(userRequestDto);
            if(result == null)
            {
                return BadRequest("Invalid user data.");
            }

            return Ok(result);
        }


        [HttpGet("all")] //GetAll Endpoint
        public async Task<IActionResult> GetAllUsers()
        
        {

            var result= await _userService.GetAllUsers();//تستدعي الدالة التابعه للservice والتي هي اساسا بدورها تستخدم دالة الريبو

            if(result is null){

                return NotFound("There is no Data to Get It!..");//لماذا لاتتنفذ
            }
            return Ok(result);


        }

        [HttpGet("{id}")]// GetById Endpoint             من اين اتى حقل الid

        public async Task<IActionResult>GetUserById(int id){
        
        var result= await _userService.GetUserById(id);//تستدعي الدالة التابعه للservice والتي هي اساسا بدورها تستخدم دالة الريبو

        if(result == null){
            return NotFound("User not found!!");//اين تعرض؟

        }

        return Ok(result);


        }


    [HttpDelete("{id}")] //Delete Endpoint 

    public async Task<IActionResult>DeleteUserById(int id )
    {
          var deleted=  await _userService.DeleteUserById(id);//القيمه المخزنه هنا true or false

            if (!deleted)
                return NotFound("User is't deleted,User not found..!");//الرساله التي تنعرض في السواجر في حال لم يوجد مستخدم يمكن حذفه

            return Ok("User deleted successfully");//في حال كان المستخدم موجود وتم الحذف


    }

    [HttpDelete("all")] //DeleteAll Endpoint 

    public async Task<IActionResult>DeleteAllUesrs()
    {
          var Notdeleted=  await _userService.DeleteAllUesrs();//القيمه المخزنه هنا true or false

            if (Notdeleted)
                {
                return NotFound("Users is't deleted,All of Users are not found..!");
                
                }//الرساله التي تنعرض في السواجر في حال لم يوجد مستخدمين يمكن حذفهم

            return Ok("All of Users deleted successfully");//في حال كان المستخدمين موجود وتم الحذف


    }

    [HttpPut("{id}")] 

    public async Task<IActionResult>UpdateUser(int id,GetUserDto updatedUserDto)
    {
            //هنا الشروط هذه من اجل عرض رسائل في السواجر فقط
            if(id != updatedUserDto.Id){//يتحقق من أن رقم المستخدم المدخل في الرابط اي السواجر يطابق الرقم الموجود داخل الجسم (body)،

            return BadRequest("User ID mismatch!!!!.");//BadRequest,NotFound هذه فقط عناوين استجابة لتوضيح الخطأ وما بداخلها هو الرسالة الوصف

            }
            var updated=await _userService.UpdateUser(updatedUserDto);

            if(!updated){
             return NotFound("User is't updated ,User not found..!");//الرساله التي تنعرض في السواجر في حال لم يوجد مستخدم


            }
            return Ok("User Updated Successfully>>");//الرساله في السواجر في حال نجحت العمليه




    }







    }
}
