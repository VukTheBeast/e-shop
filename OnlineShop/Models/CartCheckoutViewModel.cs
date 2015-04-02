using System.Collections.Generic;
using OnlineShop.Logic.Entity;

namespace OnlineShop.Models
{
    public class CartCheckoutViewModel
    {
        public Cart Cart { get; set; }
        public IList<Address> Address { get; set; }
    }

    public class Test
    {
        public int Id { get; set; }
        public bool Value { get; set; }
    }
}