using PharmacyOnDuty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyOnDuty.Aplication.Interfaces.Repository
{
    public interface IPharmacyRepository:IGenericRepository<Pharmacy>
    {
        Task DeleteAllWithCityParams(string city);
        Task DeleteAllAsync();
    }
}
