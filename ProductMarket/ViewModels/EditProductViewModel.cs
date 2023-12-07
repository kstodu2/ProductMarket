using ProductMarket.Data.Enum;
using ProductMarket.Models;

namespace ProductMarket.ViewModels
{
    public class EditProductViewModel
    {
        public int ProductId { get; set;}
        public string ProductName { get; set;}
        public string ProductDescription { get; set;}
        public IFormFile Image { get; set;}
        public string? URL { get; set;}
        public double ProductPrice { get; set;}
        public ProductCategory ProductCategory { get; set;}

        public int? AddressId { get; set;}
        public Address Address { get; set;}
     
       
    }
}
