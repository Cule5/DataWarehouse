using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Shop.Repositories;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Shop
{
    class ShopRepository:IShopRepository
    {
        private readonly AppDbContext _appDbContext = null;

        public ShopRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
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

        public Task<Core.Domain.Shop.Shop> FindAsync(Core.Domain.Shop.Shop shop)
        {
            return _appDbContext.Shops.FirstOrDefaultAsync((s)=>s.Name.Equals(shop.Name)&&s.City.Equals(shop.City)&&s.PostalCode.Equals(shop.PostalCode)&&s.Type==shop.Type);
        }
    }
}
