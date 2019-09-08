using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.TransactionProduct.Repositories
{
    public interface ITransactionProductRepository
    {
        Task AddAsync(TransactionProduct transactionProduct);
    }
}
