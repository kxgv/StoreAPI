using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAPI.Common.Dtos
{
    public record PagedResponseKeysetDto<T>
    {
        public int Reference { get; init; }
        public List<T> Data { get; init; }
    }
}
