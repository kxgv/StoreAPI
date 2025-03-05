using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAPI.Infraestructure.EntityFramework.Daos
{
    public class PagedList<T>
    {
        public bool HasNext { get; set; } = false;
        public bool HasPrevious { get; set; } = false;
        public required List<T> Entities { get; set; }
    }
}
