using System.Collections.Generic;
using Help_InjecaoDependencia.Interfaces;
using Help_InjecaoDependencia.Models;

namespace Help_InjecaoDependencia.Repositories
{
    public class ProductRepository : IProductRepository
    {
        List<Product> list;
        public ProductRepository()
        {
            list = new List<Product>() {
                new Product(1, "Product A"),
                new Product(1, "Product B"),
                new Product(1, "Product C"),
            };
        }

        public List<Product> GetAll() => list;

        public Product Create(Product item)
        {
            item.Id = list.Count + 1;
            list.Add(item);
            return item;
        }

    }
}