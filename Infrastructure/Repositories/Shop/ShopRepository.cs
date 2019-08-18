using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Shop.Repositories;
using Infrastructure.EntityFramework;

namespace Infrastructure.Repositories.Shop
{
    class ShopRepository:IShopRepository
    {
        private readonly AppDbContext _appDbContext = null;

        public ShopRepository()
        {
            _appDbContext=new AppDbContext();
        }
        public async Task<Core.Domain.Shop.Shop> GetAsync(int shopId)
        {
            return await _appDbContext.Shops.FindAsync(shopId);
        }

        public async Task AddAsync(Core.Domain.Shop.Shop shop)
        {
            await _appDbContext.Shops.AddAsync(shop);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
