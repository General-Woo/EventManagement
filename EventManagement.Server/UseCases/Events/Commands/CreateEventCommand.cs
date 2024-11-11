using EventManagement.Server.Core.Entities;
using EventManagement.Shared.DataTransferObject;
using EventManagementApp.Server.Core.Interfaces;
using MediatR;
using MongoDB.Driver;

namespace EventManagement.Server.UseCases.Events.Commands
{
    public class CreateEventCommand : IRequest<Guid>
    {
        public CreateEventCommand(AddEventDto eventDto)
        {
            Name = eventDto.Name;
            Description = eventDto.Description;
            StartDate = eventDto.StartDate;
            EndDate = eventDto.EndDate;
            VenueId = eventDto.VenueId;
            Capacity = eventDto.Capacity;
            AvailableSeats = eventDto.AvailableSeats;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid VenueId { get; set; }
        public int Capacity { get; set; }
        public int AvailableSeats { get; set; }
    }

    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
    {
        private readonly IMongoCollection<Event> _events;

        public CreateEventCommandHandler(IMongoCollectionFactory collectionFactory)
        {
            _events = collectionFactory.GetCollection<Event>("Events");
        }

        public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var eventToAdd = new Event
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                VenueId = request.VenueId,
                Capacity = request.Capacity,
                AvailableSeats = request.AvailableSeats
            };

            await _events.InsertOneAsync(eventToAdd, cancellationToken: cancellationToken);
            return eventToAdd.Id;
        }
    }
}
