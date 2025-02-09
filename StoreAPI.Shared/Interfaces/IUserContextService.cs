using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAPI.Core.Shared
{
    public interface IUserContextService
    {
        string? GetUserId();
    }
}
