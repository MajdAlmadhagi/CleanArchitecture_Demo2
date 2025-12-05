using System.ComponentModel.DataAnnotations;


namespace SCleanArchitecture.SimpleAPI.Application.DTOs;

public sealed class AddProductRequestDto
{
    public string Name {get; set;}
    public double Price {get; set;}
    public string Description {get; set;}

    public int Stock {get; set;}//المخزون للمنتج


    public bool IsValid() //??
    {
        if (string.IsNullOrEmpty(Name))//string alias name for the class System.String and IsNullOrEmpty its static method from this class
        {
            return false;
        }

        if (Price==0) //or Price.HasValue if its nullable with ? double? price
        {
            return false;
        }
        

        if(string.IsNullOrEmpty(Description))
        {
            return false;
        }

        if (Stock == 0)
        {
            return false;
        }
        //مالم

        return true;
    }

}
