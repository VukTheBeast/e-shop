using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Logic.Extension
{
    public  class AddressExtension
    {
        [Required]
        [StringLength(30, ErrorMessage = "Name can not be more then 30 characters")]
        public string ContactName { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "Country can not be more then 20 characters")]
        public string Country { get; set; }
        [Required]
        [StringLength(40, ErrorMessage = "Street can not be more then 20 characters")]
        public string Street { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "City can not be more then 10 characters")]
        public string City { get; set; }
        [Required]
        [StringLength(20, ErrorMessage = "State can not be more then 20 characters")]
        public string State { get; set; }
        [Required]
        [Display(Name = "Postal code")]
        [DataType(DataType.PostalCode)]
        public int PostalCode { get; set; }
        [Required]
        [StringLength(15, ErrorMessage = "Phone can not be more then 15 characters")]
        public string Telefon { get; set; }
    }
}