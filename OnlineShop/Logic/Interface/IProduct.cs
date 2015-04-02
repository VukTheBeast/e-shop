using System.Linq;
using OnlineShop.Logic.Entity;

namespace OnlineShop.Logic.Interface
{
    public interface IProduct
    {
        Product GetProduct(int id);
        void SaveProduct(Product product);
        void RemoveProduct(int productId);
        IQueryable<Product> GetProducts(string category);
        IQueryable<Product> GetProducts(string category, string text);

    }
}
