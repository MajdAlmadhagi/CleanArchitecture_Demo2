using SCleanArchitecture.SimpleAPI.Application.Converters;
using SCleanArchitecture.SimpleAPI.Application.DTOs;
using SCleanArchitecture.SimpleAPI.Domain.Entities;
using SCleanArchitecture.SimpleAPI.Domain.Repositories;


namespace SCleanArchitecture.SimpleAPI.Application.Services;

public interface IProductService
{
    //بما ان هذه الطبقه تتعامل مع الكائنات القادمه ك DTOs فسيتم تمرير لها DTOs على عكس طبقة الInfrastructure يتم تمرير لها والتعامل مع الEntities الذي في الدومين
    //1-AddProductAsync
    Task<ProductDto> AddProduct(AddProductRequestDto productDto);

    //2-GetAllProductsAsync
    Task<List<ProductDto>> GetAllProducts();

    //3-GetProductByIdAsync
    Task<ProductDto?> GetProductById(int id);

    //4-UpdateProductAsync
    Task<bool> UpdateProduct(ProductDto updatedProductDto);

    //5-DeleteProductAsync
    Task<bool> DeleteProductById(int id);

}

internal class ProductService(IProductRepository _productRepository) : IProductService  //this constructor?
{//The implemntation of IProductService

//here we call IProductRepository Methods and Converter Methods
    //1-AddProductAsync

    /*public ProductService(IProductRepository _productRepository) //what about this constructor
    {



    }*/

    public async Task<ProductDto> AddProduct(AddProductRequestDto productDto)
    {
        try{

            //اولا نقوم بتحويل كائن الdto او تعيين قيمته لكائن الEntity لكي يتم التعامل معه في الدومين
            var productEntity=productDto.ToProductEntity();

            //ثانيا بعد التحويل نقوم باستدعاء دالة الاضافة منIProductRepository

           await _productRepository.AddProductAsync(productEntity);//now product object in Entity class = productEntity


            //ثالثا اخذ الاستجابة بعد الاضافة مباشرتا وهي اختياريه
                       // Product p=new Product(); في هذه الحاله يتم انشاء كائن جديد قيمه صفريه

            Product p=new Product //بينما الان هيئنا بقيم فعليه قمنا باضافتها حاليا
            {
                Id=productEntity.Id,
                Name=productEntity.Name, //or productDto.Name its the same values
                Description=productEntity.Description,
                Price=productEntity.Price,
                Stock=productEntity.Stock
            };//??

            var responeOfAddin=p.ToProductDto();//الان تصبح القيم الراجعه لهذه الدالة 
            /*
            الان تصبح القيم الراجعه لهذه الدالة تساوي قيم كائن Product الذي 
            في السطر السابق وهذا لانها في الconverter 
            قيم الDto هي تكون نفس قيم الProduct Entity 
            productObject.Id
            
            */

            /*
                       //ToProductDto ترجع ProductDto قيمه = قيم الكلاس Product لانها في الConverter تعتمد على 
                         Product لذلك قمنا الان بتهيئة Product بالقيم الي نريدها ديناميكيه 
                         فلو قمنا بأسناد قيم ثابته في الconverter سيتم اخذ القيم الثابته 



            */
            return responeOfAddin;




        }catch(Exception ex)
        {
          throw new ArgumentException($"Error in AddProduct: {ex.Message}", ex);
 
        }

    }

    //2-GetAllProductsAsync
    public async Task<List<ProductDto>> GetAllProducts()
    {
        try{
             // 1️⃣ جلب جميع المنتجات من الـ Repository
        var products = await _productRepository.GetAllProductsAsync();//سيتم جلب مستخدمين من نوعUser,حسب ماهو ماعرف في ريبو الدومين والتنفيذ له

            if(products == null){
                Console.WriteLine("There is no products");
             return null;

            }
        // 2️⃣ تحويل كل مستخدم (Entity) إلى UserDto
        var productDtos = products.Select(u => u.ToProductDto());//?

        // 3️⃣ إرجاع قائمة الـ DTOs
        return productDtos.ToList();//??








        }catch(Exception ex)
        {
        throw new ArgumentException($"Error in GetAllProducts: {ex.Message}", ex);

        }


    }

    //3-GetProductByIdAsync
   public async Task<ProductDto?> GetProductById(int id)
    {

        try{

            // 1️⃣ جلب  المنتج من الـ Repository

        var oneProduct=await _productRepository.GetProductByIdAsync(id);
        if(oneProduct==null)
        {
        Console.WriteLine("=======BY id there is no product with this id to Get it=========");

        }

        // 2️⃣ تحويل  منتج (Entity) إلى ProductDto

        var productDto=oneProduct.ToProductDto();

        return productDto;






        }catch(Exception ex){
            throw new ArgumentException($"Error in GetProductById: {ex.Message}", ex);

        }

    }

    //4-UpdateProductAsync
   public async Task<bool> UpdateProduct(ProductDto updatedProductDto)
    {
        try{
        
         // 1️⃣ جلب  المنتج المحدد من الـ Repository للتحقق من وجوده قبل التحديث
        var existingProduct=await _productRepository.GetProductByIdAsync(updatedProductDto.Id);
        if(existingProduct==null)
        {
        Console.WriteLine("=======BY id there is no product with this id to update it=========");
        return false;
        }
                    // 2️⃣ تحديث كائن المنتج لذي تم جلبه  بجميع متغيراته ا
        //existingProduct كائن كامل  بيكون لل Entity
        //updatedProductDto كائن من الDTO
        existingProduct.Name=updatedProductDto.Name;
        existingProduct.Price=updatedProductDto.Price;
        existingProduct.Description=updatedProductDto.Description;
        existingProduct.Stock=updatedProductDto.Stock;

        await _productRepository.UpdateProductAsync(existingProduct);
        return true;





        }
        catch(Exception ex){

            throw new ArgumentException("Unexpected Errorrrrrr!");


        }


    }

    //5-DeleteProductAsync
   public async Task<bool> DeleteProductById(int id)
    {
        try{

            
        var oneProduct=await _productRepository.GetProductByIdAsync(id);

        if(oneProduct==null)
        {
        Console.WriteLine("=======BY id there is no product with this id to delete  it=========");
        return false;
        }

       await _productRepository.DeleteProductAsync(id);
       return true;


        }catch(Exception ex)
        {
            throw new ArgumentException($"Error in DeleteProductById: {ex.Message}", ex);

        }

    }











}
public static class ProductErrors
{
    public static AddProductResponseDto InvalidRequest()
    {
        return new AddProductResponseDto();
    }
}



