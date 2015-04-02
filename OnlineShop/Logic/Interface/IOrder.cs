using System.Collections.Generic;
using OnlineShop.Logic.Entity;
using OnlineShop.Models;

namespace OnlineShop.Logic.Interface
{
    public interface IOrder
    {
        void AddOrder(int addressId, IEnumerable<CartLine> products, string login);
        void ChangeOrderStatus(int orderId);
        Order GetOrder(int orderId);
        IList<OrderModel> GetOrders(string name);
        OrderModel GetOrderModel(int orderId);
    }
}
