using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeApi.Dashboard.Libraries.DI
{
    public interface IService
    {
        Task InitialiseAsync();
    }
}
