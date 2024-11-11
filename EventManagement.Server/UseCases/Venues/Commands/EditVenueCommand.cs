using EventManagement.Server.Core.Entities;
using EventManagement.Shared.DataTransferObject;
using EventManagementApp.Server.Core.Interfaces;
using MediatR;
using MongoDB.Driver;

namespace EventManagement.Server.UseCases.Venues.Commands
{
    public class EditVenueCommand : IRequest<Unit>
    {
        public EditVenueCommand(AddVenueDto venueDto)
        {
            Name = venueDto.Name;
            Address = venueDto.Address;
            Capacity = venueDto.Capacity;
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Capacity { get; set; }
    }

    public class EditVenueCommandHandler : IRequestHandler<EditVenueCommand, Unit>
    {
        private readonly IMongoCollection<Venue> _venues;

        public EditVenueCommandHandler(IMongoCollectionFactory collectionFactory)
        {
            _venues = collectionFactory.GetCollection<Venue>("Venues");
        }

        public async Task<Unit> Handle(EditVenueCommand request, CancellationToken cancellationToken)
        {
            var venueFilter = Builders<Venue>.Filter.Eq(e => e.Id, request.Id);
            var update = Builders<Venue>.Update;
            var updateDefinitions = new List<UpdateDefinition<Venue>>();

            if (!string.IsNullOrEmpty(request.Name)) updateDefinitions.Add(update.Set(e => e.Name, request.Name));
            if (!string.IsNullOrEmpty(request.Address)) updateDefinitions.Add(update.Set(e => e.Address, request.Address));
            if (!string.IsNullOrEmpty(request.Capacity.ToString())) updateDefinitions.Add(update.Set(e => e.Capacity, request.Capacity));

            var combinedUpdateDefinition = update.Combine(updateDefinitions);

            await _venues.UpdateOneAsync(venueFilter, combinedUpdateDefinition);
            return Unit.Value;
        }
    }
}
