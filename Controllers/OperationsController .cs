using Help_InjecaoDependencia.Interfaces;
using Help_InjecaoDependencia.Services;
using Microsoft.AspNetCore.Mvc;

namespace Help_InjecaoDependencia.Controllers
{
    public class OperationsController : Controller
    {
        private readonly OperationService _operationService;
        private readonly IOperationTransient _transientOperation;
        private readonly IOperationScoped _scopedOperation;
        private readonly IOperationSingleton _singletonOperation;        

        public OperationsController(OperationService operationService,
            IOperationTransient transientOperation,
            IOperationScoped scopedOperation,
            IOperationSingleton singletonOperation)
        {
            _operationService = operationService;
            _transientOperation = transientOperation;
            _scopedOperation = scopedOperation;
            _singletonOperation = singletonOperation;          
        }

        public IActionResult Index()
        {
            // ViewBag contém serviços solicitados pelo controlador
            ViewBag.Transient = _transientOperation;
            ViewBag.Scoped = _scopedOperation;
            ViewBag.Singleton = _singletonOperation;            

            // OperationService tem seus próprios serviços solicitados
            ViewBag.Service = _operationService;
            return View();
        }

    }
}