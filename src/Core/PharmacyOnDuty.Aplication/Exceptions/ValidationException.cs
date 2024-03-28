using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyOnDuty.Aplication.Exceptions
{
    public class ValidationException:Exception
    {

        public ValidationException(string Message):base(Message)
        {
            
        }
        public ValidationException():this("Validation Error Occured")
        {
            
        }
        public ValidationException(Exception ex):this(ex.Message)
        {
            
        }
    }
}
