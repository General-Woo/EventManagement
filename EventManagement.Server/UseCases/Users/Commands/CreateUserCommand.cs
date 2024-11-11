using EventManagement.Server.Core.Entities;
using EventManagement.Shared.DataTransferObject;
using EventManagementApp.Server.Core.Interfaces;
using MediatR;
using MongoDB.Driver;

namespace EventManagement.Server.UseCases.Users.Commands
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public CreateUserCommand(AddUserDto userDto)
        {
            FullName = userDto.FullName;
            Email = userDto.Email;
            PhoneNumber = userDto.PhoneNumber;
        }

        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IMongoCollection<User> _users;

        public CreateUserCommandHandler(IMongoCollectionFactory collectionFactory)
        {
            _users = collectionFactory.GetCollection<User>("Users");
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userToAdd = new User
            {
                Id = Guid.NewGuid(),
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };

            await _users.InsertOneAsync(userToAdd, cancellationToken: cancellationToken);
            return userToAdd.Id;
        }
    }
}
