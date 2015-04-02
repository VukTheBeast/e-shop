using System;
using System.Collections.Generic;
using System.Linq;
using OnlineShop.Logic.Entity;
using OnlineShop.Logic.Interface;
using OnlineShop.Models;

namespace OnlineShop.Logic
{
    public class UserRepository : IAccess, ICategory, IProduct, IAddress, IOrder
    {
        private readonly Context _context = new Context();

        public List<Category> GetCategory()
        {
            return _context.Categories.OrderBy(x => x.Name).ToList();
        }

        public string GetCategory(int categoryId)
        {
            return _context.Categories.First(x => x.Id == categoryId).Name;
        }

        public int GetCategory(string categoryName)
        {
            return _context.Categories.First(x => x.Name == categoryName).Id;
        }

        public int GetUserId(string name)
        {
            return _context.Users.First(x => x.Email == name).Id;
        }

        public IQueryable<Product> GetProducts(string category, string text)
        {
            var categoryId = _context.Categories.FirstOrDefault(x => x.Name == category);
            if (categoryId != null)
            {
                return
                    _context.Products.Where(x => x.CategoryId == categoryId.Id && x.Name.Contains(text))
                        .OrderBy(x => x.Id);
            }
            return _context.Products.Where(x => x.Name.Contains(text)).OrderBy(x => x.Id);
        }

        public IQueryable<Product> GetProducts(string category)
        {
            var categoryId = _context.Categories.FirstOrDefault(x => x.Name == category);
            if (categoryId != null)
            {
                return _context.Products.Where(x => x.CategoryId == categoryId.Id).OrderBy(x => x.Id);
            }
            return _context.Products.Select(x => x).OrderBy(x => x.Id);
        }

        public Product GetProduct(int id)
        {
            return _context.Products.FirstOrDefault(x => x.Id == id);
        }

        public Address GetAdress(string name)
        {
            var userId = GetUserId(name);
            return _context.Addresses.FirstOrDefault(x => x.UserId == userId && x.IsPrimary);
        }

        public IQueryable<Address> GetAllAddress(string name)
        {
            var userId = GetUserId(name);
            return _context.Addresses.Where(x => x.UserId == userId).OrderBy(x => x.IsPrimary);
        }

        public Address GetAdress(int addressId)
        {
            return _context.Addresses.FirstOrDefault(x => x.Id == addressId);
        }

        public void SaveAdress(Address address)
        {
            if (address.Id == 0)
            {
                _context.Addresses.Add(address);
            }
            else
            {
                var temp = _context.Addresses.FirstOrDefault(x => x.Id == address.Id);
                temp.PostalCode = address.PostalCode;
                temp.State = address.State;
                temp.Street = address.Street;
                temp.Telefon = address.Telefon;
                temp.ContactName = address.ContactName;
                temp.Country = address.Country;
                temp.City = address.City;
            }
            SaveData();
        }

        private void SaveData()
        {
            _context.SaveChanges();
        }

        public OrderModel GetOrderModel(int orderId)
        {
            var order = GetOrder(orderId);
            var temp = order.OrderProducts.Select(x => x);
            IList<OrderItem> orderItems = new List<OrderItem>();
            foreach (var orderProduct in temp)
            {
                orderItems.Add(new OrderItem
                {
                    Id = orderProduct.ProductId,
                    Count = orderProduct.Count,
                    Image = orderProduct.Product.Image,
                    Name = orderProduct.Product.Name,
                    Price = orderProduct.Product.Price
                });
            }
            var result = new OrderModel
            {
                OrderId = order.Id,
                DateCreated = order.DateCreated,
                IsNew = order.IsNew,
                IsProcess = order.IsProcess,
                IsConfirm = order.IsConfirm,
                DateSend = order.DateSend,
                OrderItems = orderItems,
                User = order.User.Email,
                AddressId = order.AddressId
            };
            return result;
        }

        public IList<OrderModel> GetOrders(string name)
        {
            IList<OrderModel> orders = new List<OrderModel>();
            IList<Order> query;
            if (name == "")
            {
                query = _context.Orders.ToList();
            }
            else
            {
                var userId = GetUserId(name);
                query = _context.Orders.Where(x => x.UserId == userId).ToList();
            }
            foreach (var value in query)
            {
                var temp = value.OrderProducts.Select(x => x);
                IList<OrderItem> orderItems = new List<OrderItem>();
                foreach (var orderProduct in temp)
                {
                    orderItems.Add(new OrderItem
                    {
                        Id = orderProduct.ProductId,
                        Count = orderProduct.Count,
                        Image = orderProduct.Product.Image,
                        Name = orderProduct.Product.Name,
                        Price = orderProduct.Product.Price
                    });
                }
                orders.Add(new OrderModel
                {
                    OrderId = value.Id,
                    DateCreated = value.DateCreated,
                    IsNew = value.IsNew,
                    IsProcess = value.IsProcess,
                    IsConfirm = value.IsConfirm,
                    DateSend = value.DateSend,
                    OrderItems = orderItems,
                    User = value.User.Email
                });
            }
            return orders;
        }

        public void SaveProduct(Product product)
        {
            if (product.Id == 0)
            {
                _context.Products.Add(product);
            }
            else
            {
                var temp = _context.Products.First(x => x.Id == product.Id);
                temp.Name = product.Name;
                temp.Description = product.Description;
                temp.Price = product.Price;
                temp.Image = product.Image;
                temp.CategoryId = product.CategoryId;
            }
            SaveData();
        }

        public void RemoveProduct(int productId)
        {
            var product = _context.Products.First(x => x.Id == productId);
            _context.Products.Remove(product);
            SaveData();
        }

        public List<string> GetRoles()
        {
            return
                _context.Roles.Where(x => x.RoleName != "Admin")
                    .Select(x => x.RoleName)
                    .OrderBy(x => x)
                    .ToList();
        }

        public string GetRoleNameForUser(int userId)
        {
            return _context.Users.First(x => x.Id == userId).Roles.First().RoleName;
        }

        public List<AccessModel> GetAccessModel()
        {
            var result = new List<AccessModel>();
            var query = from user in _context.Users
                        select new
                        {
                            user.Id,
                            user.Email,
                            Role = user.Roles.FirstOrDefault().RoleName
                        };
            foreach (var item in query)
            {
                //if (item.Role == "Admin")
                //{
                    result.Add(new AccessModel { Id = item.Id, Email = item.Email, Role = item.Role });
                //}
            }
            return result;
        }

        public bool IsControl(string login)
        {
            var user = _context.Users.First(x => x.Email == login);
            foreach (var role in user.Roles)
            {
                if (role.RoleName == "Admin" || role.RoleName == "Moderator")
                {
                    return true;
                }
            }
            return false;
        }

        public void AddOrder(int addressId, IEnumerable<CartLine> products, string login)
        {
            var userId = GetUserId(login);
            var orderProduct = new List<OrderProduct>();
            foreach (var item in products)
            {
                orderProduct.Add(new OrderProduct { Count = item.Quantity, ProductId = item.Product.Id });
            }
            var order = new Order
            {
                DateCreated = DateTime.Now,
                IsNew = true,
                IsProcess = true,
                IsConfirm = false,
                UserId = userId,
                AddressId = addressId,
                OrderProducts = orderProduct
            };
            _context.Orders.Add(order);
            SaveData();
        }

        public void ChangeOrderStatus(int orderId)
        {
            var order = _context.Orders.First(x => x.Id == orderId);
            order.IsNew = false;
            order.IsConfirm = true;
            order.IsProcess = false;
            order.DateSend = DateTime.Now;
            SaveData();
        }

        public Order GetOrder(int orderId)
        {
            return _context.Orders.FirstOrDefault(x => x.Id == orderId);
        }

        public string GetUserName(string login)
        {
            var user = _context.Users.First(x => x.Email == login);
            return user.FirstName + " " + user.LastName;
        }


        public void AddCategory(string categoryName)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Name == categoryName);
            if (category == null)
            {
                _context.Categories.Add(new Category {Name = categoryName});
                SaveData();
            }

        }

        public bool DeleteCategory(int categoryId)
        {
            var category = _context.Categories.First(x=>x.Id==categoryId);
            if (category.Products.Count == 0)
            {
                _context.Categories.Remove(category);
                SaveData();
                return false;
            }
            return true;
        }
    }
}