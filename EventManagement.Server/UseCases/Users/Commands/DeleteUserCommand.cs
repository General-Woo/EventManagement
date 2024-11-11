using EventManagement.Server.Core.Entities;
using EventManagementApp.Server.Core.Interfaces;
using MediatR;
using MongoDB.Driver;

namespace EventManagement.Server.UseCases.Users.Commands
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IMongoCollection<User> _users;

        public DeleteUserCommandHandler(IMongoCollectionFactory collectionFactory)
        {
            _users = collectionFactory.GetCollection<User>("Users");
        }

        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var userFilter = Builders<User>.Filter.Eq(e => e.Id, request.Id);
            await _users.DeleteOneAsync(userFilter, cancellationToken);
            return Unit.Value;
        }
    }
}
