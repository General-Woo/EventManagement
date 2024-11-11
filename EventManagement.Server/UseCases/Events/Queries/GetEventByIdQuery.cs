using EventManagement.Server.Core.Entities;
using EventManagement.Shared.DataTransferObject;
using EventManagementApp.Server.Core.Interfaces;
using MediatR;
using MongoDB.Driver;

namespace EventManagement.Server.UseCases.Events.Queries
{
    public class GetEventByIdQuery : IRequest<EventDto>
    {
        public Guid Id { get; set; }

        public GetEventByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, EventDto>
    {
        private readonly IMongoCollection<Event> _events;

        public GetEventByIdQueryHandler(IMongoCollectionFactory collectionFactory)
        {
            _events = collectionFactory.GetCollection<Event>("Events");
        }

        public async Task<EventDto> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<Event>.Filter.Eq(e => e.Id, request.Id);
            var myEvent = await _events.Find(filter).Project(EventMapping.EventProjection).FirstOrDefaultAsync();
            return myEvent;
        }
    }
}
