using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Logic.Extension
{
    public class ProductExtension
    {
        [Required]
        [StringLength(50, ErrorMessage = "Name can not be more then 50 characters")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(1000, ErrorMessage = "Description can not be more then 1000 characters")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Price")]
        [RegularExpression(@"\d*")]
        public int Price { get; set; }
    }
}