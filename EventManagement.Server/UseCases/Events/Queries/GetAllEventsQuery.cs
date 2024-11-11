using EventManagement.Server.Core.Entities;
using EventManagement.Shared.DataTransferObject;
using EventManagementApp.Server.Core.Interfaces;
using MediatR;
using MongoDB.Driver;

namespace EventManagement.Server.UseCases.Events.Queries
{
    public class GetAllEventsQuery : IRequest<List<EventDto>> 
    {
        public GetAllEventsQuery() { }
    }

    public class GetAllEventsQueryHandler : IRequestHandler<GetAllEventsQuery, List<EventDto>>
    {
        private readonly IMongoCollection<Event> _events;

        public GetAllEventsQueryHandler(IMongoCollectionFactory collectionFactory)
        {
            _events = collectionFactory.GetCollection<Event>("Events");
        }

        public async Task<List<EventDto>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<Event>.Filter.Empty;
            var events = await _events.Find(filter).Project(EventMapping.EventProjection).ToListAsync();
            return events;
        }
    }
}
