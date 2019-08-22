using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Shop.Repositories
{
    public interface IShopRepository
    {
        Task<Shop> GetAsync(int shopId);
        Task AddAsync(Shop shop);
        Task<Shop> FindAsync(Shop shop);
    }
}
