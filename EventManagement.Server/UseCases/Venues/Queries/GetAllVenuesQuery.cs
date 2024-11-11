using EventManagement.Server.Core.Entities;
using EventManagement.Shared.DataTransferObject;
using EventManagementApp.Server.Core.Interfaces;
using MediatR;
using MongoDB.Driver;

namespace EventManagement.Server.UseCases.Venues.Queries
{
    public class GetAllVenuesQuery : IRequest<List<VenueDto>> 
    {
        public GetAllVenuesQuery() { }
    }

    public class GetAllVenuesQueryHandler : IRequestHandler<GetAllVenuesQuery, List<VenueDto>>
    {
        private readonly IMongoCollection<Venue> _venues;

        public GetAllVenuesQueryHandler(IMongoCollectionFactory collectionFactory)
        {
            _venues = collectionFactory.GetCollection<Venue>("Venues");
        }

        public async Task<List<VenueDto>> Handle(GetAllVenuesQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<Venue>.Filter.Empty;
            var venues = await _venues.Find(filter).Project(VenueMapping.VenueProjection).ToListAsync();
            return venues;
        }
    }
}
