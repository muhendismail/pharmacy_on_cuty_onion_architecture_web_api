using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyOnDuty.Aplication.Wrapper
{
    public class BaseResponse
    {

        public String Message { get; set; }

        public bool IsSuccess { get; set; } = true;

    }
}
