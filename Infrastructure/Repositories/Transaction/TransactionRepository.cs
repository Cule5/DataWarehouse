using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Transaction.Repositories;
using Infrastructure.EntityFramework;

namespace Infrastructure.Repositories.Transaction
{
    class TransactionRepository:ITransactionRepositories
    {
        private readonly AppDbContext _appDbContext = null;
        public TransactionRepository()
        {
            _appDbContext=new AppDbContext();
        }
        public async Task<Core.Domain.Transaction.Transaction> GetAsync(int transactionId)
        {
            return await _appDbContext.Transactions.FindAsync(transactionId);
        }

        public async Task AddAsync(Core.Domain.Transaction.Transaction transaction)
        {
            await _appDbContext.Transactions.AddAsync(transaction);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
