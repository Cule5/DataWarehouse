using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.TransactionProduct
{
    public class TransactionProduct
    {
        public TransactionProduct()
        {

        }
        public TransactionProduct(Product.Product product,Transaction.Transaction transaction)
        {
            Product = product;
            Transaction = transaction;
        }
        public int TransactionId { get; set; }
        public virtual Transaction.Transaction Transaction { get; set; }
        public int ProductId { get; set; }
        public virtual Product.Product Product { get; set; }
    }
}
