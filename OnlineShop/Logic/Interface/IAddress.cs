using System.Linq;
using OnlineShop.Logic.Entity;

namespace OnlineShop.Logic.Interface
{
   public interface IAddress
    {
        Address GetAdress(string name);
        Address GetAdress(int addressId);
        IQueryable<Address> GetAllAddress(string name);
        void SaveAdress(Address address);

    }
}
