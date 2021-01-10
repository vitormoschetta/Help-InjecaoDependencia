using System.Collections.Generic;
using Help_InjecaoDependencia.Models;

namespace Help_InjecaoDependencia.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        Product Create(Product item);
    }
}