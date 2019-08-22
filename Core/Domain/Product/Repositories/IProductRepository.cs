using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Product.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetAsync(int productId);
        Task AddAsync(Product product);
        Task<Product> FindAsync(Product product);
    }
}
