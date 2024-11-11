using EventManagement.Server.Core.Entities;
using EventManagement.Shared.DataTransferObject;
using EventManagementApp.Server.Core.Interfaces;
using MediatR;
using MongoDB.Driver;

namespace EventManagement.Server.UseCases.Venues.Queries
{
    public class GetVenueByIdQuery : IRequest<VenueDto>
    {
        public Guid Id { get; set; }

        public GetVenueByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetVenueByIdQueryHandler : IRequestHandler<GetVenueByIdQuery, VenueDto>
    {
        private readonly IMongoCollection<Venue> _venues;

        public GetVenueByIdQueryHandler(IMongoCollectionFactory collectionFactory)
        {
            _venues = collectionFactory.GetCollection<Venue>("Venues");
        }

        public async Task<VenueDto> Handle(GetVenueByIdQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<Venue>.Filter.Eq(v => v.Id, request.Id);
            var venue = await _venues.Find(filter).Project(VenueMapping.VenueProjection).FirstOrDefaultAsync();
            return venue;
        }
    }
}
