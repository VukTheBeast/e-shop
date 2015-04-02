using OnlineShop.Logic.Entity;
using OnlineShop.Models;

namespace OnlineShop.Logic
{
    public interface IEmail
    {
        bool Register(string email, string password);
        bool ResetPassword(string email, string token);
        bool ChangePassword(string email, string newPassword);
        bool RestorePassword(string email, string newPassword);
        bool NewOrder(string userName, string email, Cart cart, Address address);
        bool OrderProcess(string userName, string email, Order order);
    }
}
