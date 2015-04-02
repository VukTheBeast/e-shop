using System.ComponentModel.DataAnnotations;
using OnlineShop.Logic.Extension;

namespace OnlineShop.Logic.Entity
{
    [MetadataType(typeof(AddressExtension))]
    public partial class Address { }

    [MetadataType(typeof(ProductExtension))]
    public partial class Product { }
}