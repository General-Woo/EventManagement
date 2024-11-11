using EventManagement.Server.Core.Entities;
using EventManagement.Shared.DataTransferObject;
using EventManagementApp.Server.Core.Interfaces;
using MediatR;
using MongoDB.Driver;

namespace EventManagement.Server.UseCases.Users.Queries
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public Guid Id { get; set; }

        public GetUserByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IMongoCollection<User> _users;

        public GetUserByIdQueryHandler(IMongoCollectionFactory collectionFactory)
        {
            _users = collectionFactory.GetCollection<User>("Users");
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, request.Id);
            var user = await _users.Find(filter).Project(UserMapping.UserProjection).FirstOrDefaultAsync();
            return user;
        }
    }
}
