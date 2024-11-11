using EventManagement.Shared.DataTransferObject;
using MediatR;

namespace EventManagement.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Guid> CreateUser(AddUserDto request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/user", request);
            return await result.Content.ReadFromJsonAsync<Guid>();
        }

        public async Task<Unit> DeleteUser(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"api/user/delete/{id}");
            return await result.Content.ReadFromJsonAsync<Unit>();
        }

        public async Task<Unit> EditUser(Guid id, AddUserDto request)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/user/{id}", request);
            return await result.Content.ReadFromJsonAsync<Unit>();
        }

        public async Task<UserDto> GetUserById(Guid id)
        {
            var result = await _httpClient.GetAsync($"api/user/{id}");
            return await result.Content.ReadFromJsonAsync<UserDto>();
        }

        public async Task<List<UserDto>> GetUserList()
        {
            var result = await _httpClient.GetAsync("api/user/list");
            return await result.Content.ReadFromJsonAsync<List<UserDto>>();
        }
    }
}
