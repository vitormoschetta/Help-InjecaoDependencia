using System.Collections.Generic;
using Help_InjecaoDependencia.Models;

namespace Help_InjecaoDependencia.Repositories
{
    public class CustommerRepository
    {
        List<Custommer> list;
        public CustommerRepository()
        {
            list = new List<Custommer>() {
                new Custommer(1, "Custommer A"),
                new Custommer(1, "Custommer B"),
                new Custommer(1, "Custommer C"),
            };
        }

        public List<Custommer> GetAll() => list;

        public Custommer Create(Custommer item)
        {
            item.Id = list.Count + 1;
            list.Add(item);
            return item;
        }
    }
}