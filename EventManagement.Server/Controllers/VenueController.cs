using Microsoft.AspNetCore.Mvc;
using MediatR;
using EventManagement.Shared.DataTransferObject;
using EventManagement.Server.UseCases.Venues.Queries;
using EventManagement.Server.UseCases.Venues.Commands;

namespace Project.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VenueController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VenueController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("list")]
        public async Task<List<VenueDto>> GetAllVenues()
        {
            return await _mediator.Send(new GetAllVenuesQuery());
        }

        [HttpGet("{id}")]
        public async Task<VenueDto> GetVenueById([FromRoute] Guid id)
        {
            return await _mediator.Send(new GetVenueByIdQuery(id));
        }

        [HttpPost]
        public async Task<Guid> CreateVenue([FromBody] AddVenueDto request)
        {
            return await _mediator.Send(new CreateVenueCommand(request));
        }

        [HttpPut("{id}")]
        public async Task<Unit> EditVenue([FromRoute] Guid id, [FromBody] AddVenueDto request)
        {
            return await _mediator.Send(new EditVenueCommand(request) { Id = id });
        }

        [HttpDelete("delete/{id}")]
        public async Task<Unit> DeleteVenue([FromRoute] Guid id)
        {
            return await _mediator.Send(new DeleteVenueCommand { Id = id });
        }
    }
}
