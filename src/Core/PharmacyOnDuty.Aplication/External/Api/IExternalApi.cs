using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyOnDuty.Application.External.Api
{
    public interface IExternalApi
    {
        Task<T?> Send<T>(HttpMethod httpMethod, string path) where T : class;
    }
}
