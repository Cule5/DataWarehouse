using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Product.Factories
{
    public interface IProductFactory
    {
        Task<Product> CreateAsync(double price,string name,int quantity);
    }
}
