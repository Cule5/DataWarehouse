using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Product
{
    public class Product
    {
        public int ProductId { get; set; }
        public double Price { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public ICollection<Transaction.Transaction> Transaction { get; set; }
        public override bool Equals(object obj)
        {
            if (!(obj is Product product))
                return false;
            return Math.Abs(product.Price - Price) < 0.001 && product.Name.Equals(Name) && product.Quantity == Quantity;

        }
    }
}
