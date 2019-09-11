using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.TransactionProduct.Factories
{
    public interface ITransactionProductFactory
    {
        Task<TransactionProduct> CreateAsync(Product.Product product,Transaction.Transaction transaction);
    }
}
