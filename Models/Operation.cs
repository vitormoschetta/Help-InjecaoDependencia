using System;
using Help_InjecaoDependencia.Interfaces;

namespace Help_InjecaoDependencia.Models
{
    public class Operation : IOperationTransient, IOperationScoped, IOperationSingleton
    {
        public Operation(Guid id)
        {
            Id = id;
        }
        public Operation()
        {
            Id = Guid.NewGuid();
        }
     
        public Guid Id { get; set; }  
      
    }
}