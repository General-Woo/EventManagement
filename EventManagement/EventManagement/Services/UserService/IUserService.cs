using EventManagement.Shared.DataTransferObject;
using MediatR;

namespace EventManagement.Services.UserService
{
    public interface IUserService
    {
        Task<List<UserDto>> GetUserList();
        Task<UserDto> GetUserById(Guid id);
        Task<Guid> CreateUser(AddUserDto request);
        Task<Unit> EditUser(Guid id, AddUserDto request);
        Task<Unit> DeleteUser(Guid id);
    }
}
