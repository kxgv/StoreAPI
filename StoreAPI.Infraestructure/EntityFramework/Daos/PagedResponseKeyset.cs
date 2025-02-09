using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAPI.Infraestructure.EntityFramework.Daos
{
    public record PagedResponseKeyset<T>
    {
        public int Reference { get; init; }
        public List<T> Data { get; init; }

        // Other properties might be added;

        public PagedResponseKeyset(List<T> data, int reference)
        {
            Data = data;
            Reference = reference;
        }
    }
}
