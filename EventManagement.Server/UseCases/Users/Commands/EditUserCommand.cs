using EventManagement.Server.Core.Entities;
using EventManagement.Shared.DataTransferObject;
using EventManagementApp.Server.Core.Interfaces;
using MediatR;
using MongoDB.Driver;

namespace EventManagement.Server.UseCases.Users.Commands
{
    public class EditUserCommand : IRequest<Unit>
    {
        public EditUserCommand(AddUserDto userDto)
        {
            FullName = userDto.FullName;
            Email = userDto.Email;
            PhoneNumber = userDto.PhoneNumber;
        }
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class EditUserCommandHandler : IRequestHandler<EditUserCommand, Unit>
    {
        private readonly IMongoCollection<User> _users;

        public EditUserCommandHandler(IMongoCollectionFactory collectionFactory)
        {
            _users = collectionFactory.GetCollection<User>("Users");
        }

        public async Task<Unit> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var userFilter = Builders<User>.Filter.Eq(e => e.Id, request.Id);
            var update = Builders<User>.Update;
            var updateDefinitions = new List<UpdateDefinition<User>>();

            if (!string.IsNullOrEmpty(request.FullName)) updateDefinitions.Add(update.Set(e => e.FullName, request.FullName));
            if (!string.IsNullOrEmpty(request.Email)) updateDefinitions.Add(update.Set(e => e.Email, request.Email));
            if (!string.IsNullOrEmpty(request.PhoneNumber)) updateDefinitions.Add(update.Set(e => e.PhoneNumber, request.PhoneNumber));

            var combinedUpdateDefinition = update.Combine(updateDefinitions);

            await _users.UpdateOneAsync(userFilter, combinedUpdateDefinition);
            return Unit.Value;
        }
    }
}
