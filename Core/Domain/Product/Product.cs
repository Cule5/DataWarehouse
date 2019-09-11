using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Product
{
    public class Product
    {
        public Product()
        {

        }
        public Product(double price,string name,int quantity)
        {
            Price = price;
            Name = name;
            Quantity = quantity;
        }
        public int ProductId { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public ICollection<TransactionProduct.TransactionProduct> TransactionProducts { get; set; }=new List<TransactionProduct.TransactionProduct>();
        public override bool Equals(object obj)
        {
            if (!(obj is Product product))
                return false;
            return Math.Abs(product.Price - Price) < 0.001 && product.Name.Equals(Name) && product.Quantity == Quantity;

        }
    }
}
