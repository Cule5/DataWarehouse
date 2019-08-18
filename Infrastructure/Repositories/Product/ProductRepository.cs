using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Product.Repositories;
using Infrastructure.EntityFramework;

namespace Infrastructure.Repositories.Product
{
    public class ProductRepository:IProductRepository
    {
        private readonly AppDbContext _appDbContext = null;
        public ProductRepository()
        {
            _appDbContext=new AppDbContext();
        }
        public async Task<Core.Domain.Product.Product> GetAsync(int productId)
        {
            return await _appDbContext.Products.FindAsync(productId);
        }

        public async Task AddAsync(Core.Domain.Product.Product product)
        {
            await _appDbContext.Products.AddAsync(product);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
