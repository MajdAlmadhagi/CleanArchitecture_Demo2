using SCleanArchitecture.SimpleAPI.Application.Converters;
using SCleanArchitecture.SimpleAPI.Application.DTOs;
using SCleanArchitecture.SimpleAPI.Domain.Repositories;

namespace SCleanArchitecture.SimpleAPI.Application.Services;

public interface IUserService //كلاس مجرد يتم تنفيذه في الابن بحيث نعتبره انه غلاف بداخله يتم استخدام دوال طبقة الدومين
{
    //1-AddUserAsync 
    Task<AddUserResponseDto> AddUser(AddUserRequestDto requestDto);

    //2-GetAllUserAsync
    Task<List<GetUserDto?>> GetAllUsers();//سوف يتم ارجاع كائنات الDto ,وليس الEntity 

    //3-GetUserByIdAsync
    Task<GetUserDto> GetUserById(int id);//<User?>

    //4-DeleteUserAsync
    Task<bool> DeleteUserById(int id);

    //5-UpdateUserAsync

    Task<bool> UpdateUser(GetUserDto updatedUserDto);

    //6-DeleteAllUesrsAsync

    Task<bool> DeleteAllUesrs();


}

internal  class UserService(IUserRepository _userRepository) : IUserService 
{

   //1-
    public async Task<AddUserResponseDto> AddUser(AddUserRequestDto requestDto)
    {
        try
        {
            if (!requestDto.IsValid())
            {
                return UserErrors.InvalidRequest();
            }

            var userEntity = requestDto.ToUserEntity();

            await _userRepository.AddUserAsync(userEntity);

            var response = requestDto.ToAddUserResponse(userEntity.CreatedAt);

            return response;



        }catch(Exception ex)
        {
            throw new ArgumentException($"Error in AddUser: {ex.Message}", ex);
        }
    }

    //2-

        public async Task<List<GetUserDto?>> GetAllUsers()
        {

            try{




                 // 1️⃣ جلب جميع المستخدمين من الـ Repository
        var users = await _userRepository.GetAllUsersAsync();//سيتم جلب مستخدمين من نوعUser,حسب ماهو ماعرف في ريبو الدومين والتنفيذ له

            /*if(users == null){
                Console.WriteLine("There is no users");
             return null;

            }*/
        // 2️⃣ تحويل كل مستخدم (Entity) إلى UserDto
        var userDtos = users.Select(u => u.ToUserDto());//?

        // 3️⃣ إرجاع قائمة الـ DTOs
        return userDtos.ToList();//??



                /*var userDtos=user.ToUserDto;
                await _userRepository.GetAllUserAsync();
                
                return userDtos;*/



            }catch(Exception ex){
                 throw new ArgumentException($"Error in GetAllUsers: {ex.Message}", ex);

            }








        }


    //3-

        public async Task<GetUserDto> GetUserById(int id)
        {
            try{

            // 1️⃣ جلب  المستخدم من الـ Repository

            var oneUser = await _userRepository.GetUserByIdAsync(id);//سيتم جلب مستخدم واحد من نوعUser,حسب ماهو ماعرف في ريبو الدومين والتنفيذ له
            if(oneUser == null){
                Console.WriteLine("=======BY id there is no user with this id to Get it=========");

                return null;
            }
             // 2️⃣ تحويل كل مستخدم (Entity) إلى UserDto
            var userDto = oneUser.ToUserDto();//?

            return userDto;





            }catch(Exception ex)
            
            {
                throw new ArgumentException($"Error in GetUserById: {ex.Message}", ex);
            }




        }



    //4-

    public async Task<bool> DeleteUserById(int id)
        {
            try{

            // 1️⃣ جلب  المستخدم المحدد من الـ Repository للتحقق من وجوده قبل الحذف

            var oneUser = await _userRepository.GetUserByIdAsync(id);//سيتم جلب مستخدم واحد من نوعUser,حسب ماهو ماعرف في ريبو الدومين والتنفيذ له

            //GetUserByIdAsync>>هنا استدعيناها فقط من اجل التحقق وارجاع قيمه من هذا التحقق والا يمكن الاستغناء عنها
            if(oneUser is null){

                 Console.WriteLine("=======BY id there is no user with this id to Delete it=========");

                return false;

            }
             // 2️⃣ حذف المستخدم الذي تم جلبه 

            await _userRepository.DeleteUserAsync(id);//سيتم حذف مستخدم واحد من نوعUser,حسب ماهو ماعرف في ريبو الدومين والتنفيذ له
            return true;




            }catch(Exception ex)
            
            {
                throw new ArgumentException($"Error in DeleteUserById: {ex.Message}", ex);
            }




        }


        //5-

          public async Task<bool> UpdateUser(GetUserDto updatedUserDto)
          {


        try
        {

            // 1️⃣ جلب  المستخدم المحدد من الـ Repository للتحقق من وجوده قبل التحديث

          var existingUser = await _userRepository.GetUserByIdAsync(updatedUserDto.Id);

          if(existingUser is null){
            //Console.WriteLine("=======BY id there is no user with this id to Delete it=========");

            return false;
            }


            // 2️⃣ تحديث المستخدم الذي تم جلبه 


            existingUser.Name = updatedUserDto.Name;//قادمة من السواجر Dto 
            existingUser.Email = updatedUserDto.Email;

            await _userRepository.UpdateUserAsync(existingUser);
            return true;





        }
        catch (Exception ex)
        {
            throw new ArgumentException($"Error in UpdateUser: {ex.Message}", ex);
        }


            


          }


          //6-

          public async Task<bool> DeleteAllUesrs()
          {
        try{

                 //اولا نجلب جميع المستخدمين المراد حذفهم للتحقق من وجودهم
             
           var users=await _userRepository.GetAllUsersAsync();
           if(users == null){
            Console.WriteLine("The Table is Empty , There is no users For deleting");
            return false;
           }

           //ثانيا بعد التحقق نقوم بأستدعاء دالة الحذف من الريبو
         await _userRepository.DeleteAllUesrsAsync();
         return true;



            }catch(Exception ex){

                throw new ArgumentException($"Error in DeleteAllUesrs: {ex.Message}", ex);


                }

           

          }
}



public static class UserErrors  //??
{
    public static AddUserResponseDto InvalidRequest()
    {
        return new AddUserResponseDto();
    }
}
