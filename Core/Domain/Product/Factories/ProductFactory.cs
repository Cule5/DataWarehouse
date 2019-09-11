using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Product.Repositories;

namespace Core.Domain.Product.Factories
{
    public class ProductFactory:IProductFactory
    {
        private readonly IProductRepository _productRepository = null;
        public ProductFactory(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Product> CreateAsync(double price, string name, int quantity)
        {
            var product = new Product(price,name,quantity);
            var resultProduct=await _productRepository.FindAsync(product)??product;
            return resultProduct;
        }
    }
}
