using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.TransactionProduct.Factories
{
    public class TransactionProductFactory:ITransactionProductFactory
    {
        public Task<TransactionProduct> CreateAsync(Product.Product product, Transaction.Transaction transaction)
        {
            return Task.Factory.StartNew(() =>
            {
                var transactionProduct=new TransactionProduct(product,transaction);
                return transactionProduct;
            });
        }
    }
}
