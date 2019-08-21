using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.TransactionProduct
{
    public class TransactionProduct
    {
        public Transaction.Transaction Transaction { get; set; }
        public Product.Product Product { get; set; }
    }
}
