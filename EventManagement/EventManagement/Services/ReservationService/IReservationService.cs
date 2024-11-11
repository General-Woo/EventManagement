using EventManagement.Shared.DataTransferObject;
using MediatR;

namespace EventManagement.Services.ReservationService
{
    public interface IReservationService
    {
        Task<List<ReservationDto>> GetReservationList();
        Task<ReservationDto> GetReservationById(Guid id);
        Task<Guid> CreateReservation(AddReservationDto request);
        Task<Unit> EditReservation(Guid id, AddReservationDto request);
        Task<Unit> DeleteReservation(Guid id);
    }
}
