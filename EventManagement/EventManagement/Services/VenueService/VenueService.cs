using EventManagement.Shared.DataTransferObject;
using MediatR;

namespace EventManagement.Services.VenueService
{
    public class VenueService : IVenueService
    {
        private readonly HttpClient _httpClient;

        public VenueService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Guid> CreateVenue(AddVenueDto request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/venue", request);
            return await result.Content.ReadFromJsonAsync<Guid>();
        }

        public async Task<Unit> DeleteVenue(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"api/venue/delete/{id}");
            return await result.Content.ReadFromJsonAsync<Unit>();
        }

        public async Task<Unit> EditVenue(Guid id, AddVenueDto request)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/venue/{id}", request);
            return await result.Content.ReadFromJsonAsync<Unit>();
        }

        public async Task<VenueDto> GetVenueById(Guid id)
        {
            var result = await _httpClient.GetAsync($"api/venue/{id}");
            return await result.Content.ReadFromJsonAsync<VenueDto>();
        }

        public async Task<List<VenueDto>> GetVenueList()
        {
            var result = await _httpClient.GetAsync("api/venue/list");
            return  await result.Content.ReadFromJsonAsync<List<VenueDto>>();
        }
    }
}
