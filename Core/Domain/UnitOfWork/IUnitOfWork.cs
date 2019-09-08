using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Product.Repositories;
using Core.Domain.Shop.Repositories;
using Core.Domain.Transaction.Repositories;

namespace Core.Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        
        Task SaveAsync();
    }
}
