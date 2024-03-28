using PharmacyOnDuty.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyOnDuty.Domain.Entities
{
    public class Pharmacy: BaseEntity
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? District { get; set; }
        public string? Phone { get; set; }
        public string? Loc { get; set; }
        public string? City { get; set; }
    }
}
