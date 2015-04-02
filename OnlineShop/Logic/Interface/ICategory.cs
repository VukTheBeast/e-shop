using System.Collections.Generic;
using OnlineShop.Logic.Entity;

namespace OnlineShop.Logic.Interface
{
    public interface ICategory
    {
        List<Category> GetCategory();
        string GetCategory(int categoryId);
        int GetCategory(string categoryName);
        void AddCategory(string categoryName);
        bool DeleteCategory(int categoryId);

    }
}
