using EventManagement.Shared.DataTransferObject;
using MediatR;

namespace EventManagement.Services.EventService
{
    public class EventService : IEventService
    {
        private readonly HttpClient _httpClient;

        public EventService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Guid> CreateEvent(AddEventDto request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/event", request);
            return await result.Content.ReadFromJsonAsync<Guid>();
        }

        public async Task<Unit> DeleteEvent(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"api/event/delete/{id}");
            return await result.Content.ReadFromJsonAsync<Unit>();
        }

        public async Task<Unit> EditEvent(Guid id, AddEventDto request)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/event/{id}", request);
            return await result.Content.ReadFromJsonAsync<Unit>();
        }

        public async Task<EventDto> GetEventById(Guid id)
        {
            var result = await _httpClient.GetAsync($"api/event/{id}");
            return await result.Content.ReadFromJsonAsync<EventDto>();
        }

        public async Task<List<EventDto>> GetEventList()
        {
            var result = await _httpClient.GetAsync("api/event/list");
            return await result.Content.ReadFromJsonAsync<List<EventDto>>();
        }
    }
}
