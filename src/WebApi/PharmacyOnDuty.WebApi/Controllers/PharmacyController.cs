using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmacyOnDuty.Aplication.Wrapper;
using PharmacyOnDuty.Application.Features.Queries.GetAllPharmaciesQuery;
using PharmacyOnDuty.Domain.Entities;
using System.Runtime.InteropServices;

namespace PharmacyOnDuty.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacyController : ControllerBase
    {
        private readonly ILogger<PharmacyController> _logger;
        private readonly IMediator _mediator;
        public PharmacyController(IMediator mediator,  ILogger<PharmacyController> logger)
        {
            _logger = logger;
            _mediator= mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetPharmacies([FromQuery] GetAllPharmacyQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("GetFromExternalApi")]
        public async Task<IActionResult> GetPharmaciesFromApi()
        {
            return Ok();
        }
    }
}
