using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Transaction.Repositories;
using Infrastructure.EntityFramework;

namespace Infrastructure.Repositories.Transaction
{
    class TransactionRepository:ITransactionRepository
    {
        private readonly AppDbContext _appDbContext = null;
        public TransactionRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Core.Domain.Transaction.Transaction> GetAsync(int transactionId)
        {

            return await _appDbContext.Transactions.FindAsync(transactionId);
        }

        public async Task AddAsync(Core.Domain.Transaction.Transaction transaction)
        {
            if(transaction==null)
                throw new ArgumentNullException();
            await _appDbContext.Transactions.AddAsync(transaction);
        }
    }
}
