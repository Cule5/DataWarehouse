using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Transaction.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction> GetAsync(int transactionId);
        Task AddAsync(Transaction transaction);
    }
}
