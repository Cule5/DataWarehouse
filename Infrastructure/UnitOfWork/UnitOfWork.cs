﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Product.Repositories;
using Core.Domain.Shop.Repositories;
using Core.Domain.Transaction.Repositories;
using Core.Domain.UnitOfWork;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        public UnitOfWork(AppDbContext dbContext)
        {
            _appDbContext = dbContext;
        }
        public ITransactionRepository TransactionRepository { get; set; }
        public IProductRepository ProductRepository { get; set; }
        public IShopRepository ShopRepository { get; set; }

        public async Task SaveAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _appDbContext.Dispose();
        }
    }
}
