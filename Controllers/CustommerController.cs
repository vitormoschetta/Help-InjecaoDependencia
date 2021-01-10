using System.Collections.Generic;
using Help_InjecaoDependencia.Models;
using Help_InjecaoDependencia.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Help_InjecaoDependencia.Controllers
{
    public class CustommerController : Controller
    {
        private CustommerRepository _repository = new CustommerRepository();

        public IActionResult Index()
        {
            List<Custommer> custommers = _repository.GetAll();
            return View(custommers);
        } 

        public IActionResult Create() => View();

    }
}