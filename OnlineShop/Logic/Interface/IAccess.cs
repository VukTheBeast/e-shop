using System.Collections.Generic;
using OnlineShop.Models;

namespace OnlineShop.Logic.Interface
{
    public interface IAccess
    {
        int GetUserId(string name);
        List<string> GetRoles();
        List<AccessModel> GetAccessModel();
        bool IsControl(string login);
        string GetUserName(string login);
    }
}
