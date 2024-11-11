using EventManagement.Server.Core.Entities;
using EventManagement.Shared.DataTransferObject;
using EventManagementApp.Server.Core.Interfaces;
using MediatR;
using MongoDB.Driver;

namespace EventManagement.Server.UseCases.Events.Commands
{
    public class EditEventCommand : IRequest<Unit>
    {
        public EditEventCommand(AddEventDto eventDto)
        {
            Name = eventDto.Name;
            Description = eventDto.Description;
            StartDate = eventDto.StartDate;
            EndDate = eventDto.EndDate;
            Capacity = eventDto.Capacity;
            AvailableSeats = eventDto.AvailableSeats;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Capacity { get; set; }
        public int AvailableSeats { get; set; }
    }

    public class EditEventCommandHandler : IRequestHandler<EditEventCommand, Unit>
    {
        private readonly IMongoCollection<Event> _events;

        public EditEventCommandHandler(IMongoCollectionFactory collectionFactory)
        {
            _events = collectionFactory.GetCollection<Event>("Events");
        }

        public async Task<Unit> Handle(EditEventCommand request, CancellationToken cancellationToken)
        {
            var eventFilter = Builders<Event>.Filter.Eq(e => e.Id, request.Id);
            var update = Builders<Event>.Update;
            var updateDefinitions = new List<UpdateDefinition<Event>>();

            if (!string.IsNullOrEmpty(request.Name))                        updateDefinitions.Add(update.Set(e => e.Name, request.Name));
            if (!string.IsNullOrEmpty(request.Description))                 updateDefinitions.Add(update.Set(e => e.Description, request.Description));
            if (!string.IsNullOrEmpty(request.StartDate.ToString()))        updateDefinitions.Add(update.Set(e => e.StartDate, request.StartDate));
            if (!string.IsNullOrEmpty(request.EndDate.ToString()))          updateDefinitions.Add(update.Set(e => e.EndDate, request.EndDate));
            if (!string.IsNullOrEmpty(request.Capacity.ToString()))         updateDefinitions.Add(update.Set(e => e.Capacity, request.Capacity));
            if (!string.IsNullOrEmpty(request.AvailableSeats.ToString()))   updateDefinitions.Add(update.Set(e => e.AvailableSeats, request.AvailableSeats));

            var combinedUpdateDefinition = update.Combine(updateDefinitions);

            await _events.UpdateOneAsync(eventFilter, combinedUpdateDefinition);
            return Unit.Value;
        }
    }
}
