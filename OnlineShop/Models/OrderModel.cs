using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OnlineShop.Logic.Entity;

namespace OnlineShop.Models
{
    public class OrderDetailsModel
    {
        public OrderModel OrderModel { get; set; }
        public Address Address { get; set; }    
    }

    public class OrderModel
    {
        public int OrderId { get; set; }
        public System.DateTime DateCreated { get; set; }
        public bool IsNew { get; set; }
        public bool IsProcess { get; set; }
        public bool IsConfirm { get; set; }
        public Nullable<System.DateTime> DateSend { get; set; }
        public string User { get; set; }
        public int AddressId { get; set; }
        public IList<OrderItem> OrderItems { get; set; }
        public decimal ComputeTotalValue()
        {
            return OrderItems.Sum(e => e.Price * e.Count);
        }
    }

    public class OrderItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public int Count { get; set; }
    }
}