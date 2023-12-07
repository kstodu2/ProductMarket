using ProductMarket.Data.Enum;
using ProductMarket.Models;

namespace ProductMarket.ViewModels
{
    public class CreateProductViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public IFormFile Image { get; set; }
        public Address Address { get; set; }
        public double ProductPrice { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public string AppUserId {  get; set; }
    }

}
