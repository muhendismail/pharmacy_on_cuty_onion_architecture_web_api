using PharmacyOnDuty.Aplication.Wrapper;
using PharmacyOnDuty.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyOnDuty.Application.Wrapper
{
    public class ServiceResponse<T> : BaseResponse
    {
        public T Data { get; set; }
        public ServiceResponse(T value)
        {
            Data = value;
        }

    }
}
