using AutoMapper;
using MediatR;
using PharmacyOnDuty.Aplication.Interfaces.Repository;
using PharmacyOnDuty.Application.Cache;
using PharmacyOnDuty.Application.Dtos;
using PharmacyOnDuty.Application.Services;
using PharmacyOnDuty.Application.Wrapper;
using PharmacyOnDuty.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyOnDuty.Application.Features.Queries.GetAllPharmaciesQuery
{
    public class GetAllPharmacyQuery: IRequest<ServiceResponse<List<PharmacyViewDto>>>
    {
        public string City { get; set; }
        public class GetAllPharcyQueryHandler : IRequestHandler<GetAllPharmacyQuery, ServiceResponse<List<PharmacyViewDto>>>
        {
            private readonly PharmacyService _service;
            private readonly IMapper _mapper;
            public GetAllPharcyQueryHandler(PharmacyService service, IMapper mapper)
            {
                _service = service;
                _mapper = mapper;
            }

            public async Task<ServiceResponse<List<PharmacyViewDto>>> Handle(GetAllPharmacyQuery request, CancellationToken cancellationToken)
            {
                List<Pharmacy> pharmacies = await _service.GetPharmacyList(request.City);
                var responseList = _mapper.Map<List<PharmacyViewDto>>(pharmacies);

                return new ServiceResponse<List<PharmacyViewDto>>(responseList);
            }
        }
    }
}
