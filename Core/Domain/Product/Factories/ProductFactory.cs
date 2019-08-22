using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Product.Factories
{
    public class ProductFactory:IProductFactory
    {
        public Task<Product> CreateAsync(Guid id, string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
