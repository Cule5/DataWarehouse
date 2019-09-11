using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Common;
using Core.Domain.Shop.Repositories;

namespace Core.Domain.Shop.Factories
{
    public class ShopFactory:IShopFactory
    {
        private readonly IShopRepository _shopRepository = null;
        public ShopFactory(IShopRepository shopRepository)
        {
            _shopRepository = shopRepository;
        }
        public async Task<Shop> CreateAsync(string name, EShopType shopType, string postCode, string city)
        {
            var shop=new Shop(name, shopType,postCode,city);
            var resultShop=await _shopRepository.FindAsync(shop)??shop;
            return resultShop;
        }
    }
}
