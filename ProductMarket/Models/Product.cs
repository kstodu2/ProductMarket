using ProductMarket.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductMarket.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public string Image { get; set; }
        public ProductCategory ProductCategory { get; set; }

        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public Address? Address { get; set; }

        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public string? AppUser { get; set;}
    }
}
