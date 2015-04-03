using System.Collections.Generic;
using System.Linq;
using OnlineShop.Logic.Entity;

namespace OnlineShop.Models
{
    public class Cart
    {
        private readonly List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Product product, int quantity)
        {
            var line = lineCollection.FirstOrDefault(p => p.Product.Id == product.Id);

            if (line == null)
            {
                lineCollection.Add(new CartLine {Product = product, Quantity = quantity});
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Product product)
        {
            var line = lineCollection.First(x => x.Product.Id == product.Id);
            if (line.Quantity > 1)
            {
                line.Quantity = line.Quantity - 1;
            }
            else
            {
                lineCollection.RemoveAll(l => l.Product.Id == product.Id);
            }
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Product.Price*e.Quantity);
        }
        public string ItemsName()
        {
            string names="";
            foreach (var item in lineCollection)
            {
                names += "[" +item.Product.Name+ "]" + System.Environment.NewLine;
            }
            return names;
            //return lineCollection.Sum(e => e.Product.Price * e.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollection; }
        }
    }

    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}