using EventManagement.Server.Core.Entities;
using EventManagementApp.Server.Core.Interfaces;
using MediatR;
using MongoDB.Driver;

namespace EventManagement.Server.UseCases.Events.Commands
{
    public class DeleteEventCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }

    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, Unit>
    {
        private readonly IMongoCollection<Event> _events;

        public DeleteEventCommandHandler(IMongoCollectionFactory collectionFactory)
        {
            _events = collectionFactory.GetCollection<Event>("Events");
        }

        public async Task<Unit> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            var eventFilter = Builders<Event>.Filter.Eq(e => e.Id, request.Id);
            await _events.DeleteOneAsync(eventFilter, cancellationToken);
            return Unit.Value;
        }
    }
}
