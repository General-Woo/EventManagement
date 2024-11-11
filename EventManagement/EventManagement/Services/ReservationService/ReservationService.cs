using EventManagement.Shared.DataTransferObject;
using MediatR;

namespace EventManagement.Services.ReservationService
{
    public class ReservationService : IReservationService
    {
        private readonly HttpClient _httpClient;

        public ReservationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Guid> CreateReservation(AddReservationDto request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/reservation", request);
            return await result.Content.ReadFromJsonAsync<Guid>();
        }

        public async Task<Unit> DeleteReservation(Guid id)
        {
            var result = await _httpClient.DeleteAsync($"api/reservation/delete/{id}");
            return await result.Content.ReadFromJsonAsync<Unit>();
        }

        public async Task<Unit> EditReservation(Guid id, AddReservationDto request)
        {
            var result = await _httpClient.PutAsJsonAsync($"api/reservation/{id}", request);
            return await result.Content.ReadFromJsonAsync<Unit>();
        }

        public async Task<ReservationDto> GetReservationById(Guid id)
        {
            var result = await _httpClient.GetAsync($"api/reservation/{id}");
            return await result.Content.ReadFromJsonAsync<ReservationDto>();
        }

        public async Task<List<ReservationDto>> GetReservationList()
        {
            var result = await _httpClient.GetAsync("api/reservation/list");
            return await result.Content.ReadFromJsonAsync<List<ReservationDto>>();
        }
    }
}
