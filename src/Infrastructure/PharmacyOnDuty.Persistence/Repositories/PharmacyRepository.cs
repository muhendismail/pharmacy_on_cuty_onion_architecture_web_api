using Microsoft.EntityFrameworkCore;
using PharmacyOnDuty.Aplication.Interfaces.Repository;
using PharmacyOnDuty.Domain.Entities;
using PharmacyOnDuty.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyOnDuty.Persistence.Repositories
{
    public class PharmacyRepository : GenericRepository<Pharmacy>, IPharmacyRepository
    {
        public PharmacyRepository(PharmacyDbContext context) : base(context)
        {
        }

        public async Task DeleteAllWithCityParams(string city)
        {
            _ = await _context.Pharmacies.Where(x => x.City.ToLower() == city.ToLower()).ExecuteDeleteAsync();

            _context.Pharmacies.RemoveRange();
        }

        public Task DeleteAllAsync()
        {
            _context.Pharmacies.ExecuteDeleteAsync();
            _context.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
