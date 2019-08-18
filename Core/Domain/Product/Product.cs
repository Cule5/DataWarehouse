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
        public int Amount { get; set; }
        public Transaction.Transaction Transaction { get; set; }
    }
}
