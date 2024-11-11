using Microsoft.AspNetCore.Mvc;
using MediatR;
using System;
using EventManagement.Shared.DataTransferObject;
using EventManagement.Server.UseCases.Events.Queries;
using EventManagement.Server.UseCases.Events.Commands;

namespace Project.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EventController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("list")]
        public async Task<List<EventDto>> GetAllEvents()
        {
            return await _mediator.Send(new GetAllEventsQuery());
        }

        [HttpGet("{id}")]
        public async Task<EventDto> GetEventById([FromRoute] Guid id)
        {
            return await _mediator.Send(new GetEventByIdQuery(id));
        }

        [HttpPost]
        public async Task<Guid> CreateEvent([FromBody] AddEventDto request)
        {
            return await _mediator.Send(new CreateEventCommand(request));
        }

        [HttpPut("{id}")]
        public async Task<Unit> EditEvent([FromRoute] Guid id, [FromBody] AddEventDto request)
        {
            return await _mediator.Send(new EditEventCommand(request) { Id = id });
        }

        [HttpDelete("delete/{id}")]
        public async Task<Unit> DeleteEvent([FromRoute] Guid id)
        {
            return await _mediator.Send(new DeleteEventCommand { Id = id });
        }
    }
}
