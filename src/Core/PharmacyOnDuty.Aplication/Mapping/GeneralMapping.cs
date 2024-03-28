using AutoMapper;
using PharmacyOnDuty.Application.Dtos;
using PharmacyOnDuty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyOnDuty.Application.Mapping
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            CreateMap<Pharmacy, PharmacyViewDto>()
                .ReverseMap();
        }
    }
}

