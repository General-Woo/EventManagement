using EventManagement.Server.Core.Entities;
using EventManagement.Shared.DataTransferObject;
using EventManagementApp.Server.Core.Interfaces;
using MediatR;
using MongoDB.Driver;

namespace EventManagement.Server.UseCases.Venues.Commands
{
    public class CreateVenueCommand : IRequest<Guid>
    {
        public CreateVenueCommand(AddVenueDto venueDto)
        {
            Name = venueDto.Name;
            Address = venueDto.Address;
            Capacity = venueDto.Capacity;
        }

        public string Name { get; set; }
        public string Address { get; set; }
        public int Capacity { get; set; }
    }

    public class CreateVenueCommandHandler : IRequestHandler<CreateVenueCommand, Guid>
    {
        private readonly IMongoCollection<Venue> _venues;

        public CreateVenueCommandHandler(IMongoCollectionFactory collectionFactory)
        {
            _venues = collectionFactory.GetCollection<Venue>("Venues");
        }

        public async Task<Guid> Handle(CreateVenueCommand request, CancellationToken cancellationToken)
        {
            var venueToAdd = new Venue
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Address = request.Address,
                Capacity = request.Capacity
            };

            await _venues.InsertOneAsync(venueToAdd, cancellationToken: cancellationToken);
            return venueToAdd.Id;
        }
    }
}
