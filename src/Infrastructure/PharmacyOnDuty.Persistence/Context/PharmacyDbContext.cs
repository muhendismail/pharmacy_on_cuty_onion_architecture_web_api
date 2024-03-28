using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PharmacyOnDuty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyOnDuty.Persistence.Context
{
    public class PharmacyDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public PharmacyDbContext(DbContextOptions<PharmacyDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Pharmacy> Pharmacies { get; set; }
    }


}

