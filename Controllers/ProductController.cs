using System.Collections.Generic;
using Help_InjecaoDependencia.Interfaces;
using Help_InjecaoDependencia.Models;
using Microsoft.AspNetCore.Mvc;

namespace Help_InjecaoDependencia.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _repository;
        public ProductController(IProductRepository repository) =>
            _repository = repository;


        public IActionResult Index()
        {
            List<Product> products = _repository.GetAll();
            return View(products);
        }

        public IActionResult Create() => View();

    }
}