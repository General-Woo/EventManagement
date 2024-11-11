using EventManagement.Server.Core.Entities;
using EventManagementApp.Server.Core.Interfaces;
using MediatR;
using MongoDB.Driver;

namespace EventManagement.Server.UseCases.Venues.Commands
{
    public class DeleteVenueCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }

    public class DeleteVenueCommandHandler : IRequestHandler<DeleteVenueCommand, Unit>
    {
        private readonly IMongoCollection<Venue> _venues;

        public DeleteVenueCommandHandler(IMongoCollectionFactory collectionFactory)
        {
            _venues = collectionFactory.GetCollection<Venue>("Venues");
        }

        public async Task<Unit> Handle(DeleteVenueCommand request, CancellationToken cancellationToken)
        {
            var venueFilter = Builders<Venue>.Filter.Eq(e => e.Id, request.Id);
            await _venues.DeleteOneAsync(venueFilter, cancellationToken);
            return Unit.Value;
        }
    }
}
