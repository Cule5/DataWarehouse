using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Common;

namespace Core.Domain.Shop.Factories
{
    public interface IShopFactory
    {
        Task<Shop> CreateAsync(string name,EShopType shopType,string postCode,string city);
    }
}
