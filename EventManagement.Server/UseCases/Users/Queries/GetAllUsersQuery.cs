using EventManagement.Server.Core.Entities;
using EventManagement.Server.UseCases.Venues;
using EventManagement.Shared.DataTransferObject;
using EventManagementApp.Server.Core.Interfaces;
using MediatR;
using MongoDB.Driver;

namespace EventManagement.Server.UseCases.Users.Queries
{
    public class GetAllUsersQuery : IRequest<List<UserDto>> 
    {
        public GetAllUsersQuery() { }
    }

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
    {
        private readonly IMongoCollection<User> _users;

        public GetAllUsersQueryHandler(IMongoCollectionFactory collectionFactory)
        {
            _users = collectionFactory.GetCollection<User>("Users");
        }

        public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<User>.Filter.Empty;
            var users = await _users.Find(filter).Project(UserMapping.UserProjection).ToListAsync();
            return users;
        }
    }
}
