using Microsoft.AspNetCore.Mvc;
using MediatR;
using EventManagement.Shared.DataTransferObject;
using EventManagement.Server.UseCases.Reservations.Queries;
using EventManagement.Server.UseCases.Reservations.Commands;

namespace Project.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReservationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("list")]
        public async Task<List<ReservationDto>> GetAllReservations()
        {
            return await _mediator.Send(new GetAllReservationsQuery());
        }

        [HttpGet("{id}")]
        public async Task<ReservationDto> GetReservationById([FromRoute] Guid id)
        {
            return await _mediator.Send(new GetReservationByIdQuery(id));
        }

        [HttpPost]
        public async Task<Guid> CreateReservation([FromBody] AddReservationDto request)
        {
            return await _mediator.Send(new CreateReservationCommand(request));
        }

        [HttpPut("{id}")]
        public async Task<Unit> EditReservation([FromRoute] Guid id, [FromBody] AddReservationDto request)
        {
            return await _mediator.Send(new EditReservationCommand(request) { Id = id });
        }

        [HttpDelete("delete/{id}")]
        public async Task<Unit> DeleteReservation([FromRoute] Guid id)
        {
            return await _mediator.Send(new DeleteReservationCommand { Id = id });
        }
    }
}
