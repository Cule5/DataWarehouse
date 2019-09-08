using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Product.Repositories;
using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Product
{
    public class ProductRepository:IProductRepository
    {
        private readonly AppDbContext _appDbContext = null;
        public ProductRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Core.Domain.Product.Product> GetAsync(int productId)
        {
            return await _appDbContext.Products.FindAsync(productId);
        }

        public async Task AddAsync(Core.Domain.Product.Product product)
        {
            if(product==null)
                throw new ArgumentNullException();
            await _appDbContext.Products.AddAsync(product);
        }

        public async Task<Core.Domain.Product.Product> FindAsync(Core.Domain.Product.Product product)
        {
            if (product == null)
                throw new ArgumentNullException();
            return await _appDbContext.Products.FirstOrDefaultAsync((p)=>p.Name.Equals(product.Name)&&Math.Abs(p.Price - product.Price) < 0.01&&p.Quantity==product.Quantity);
        }
    }
}
