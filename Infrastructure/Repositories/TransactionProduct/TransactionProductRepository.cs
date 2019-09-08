using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.TransactionProduct.Repositories;
using Infrastructure.EntityFramework;

namespace Infrastructure.Repositories.TransactionProduct
{
    public class TransactionProductRepository: ITransactionProductRepository
    {
        private AppDbContext _appDbContext;
        public TransactionProductRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task AddAsync(Core.Domain.TransactionProduct.TransactionProduct transactionProduct)
        {
            if(transactionProduct==null)
                throw new ArgumentNullException();
            await _appDbContext.TransactionProducts.AddAsync(transactionProduct);
        }
    }
}
