using EventManagement.Shared.DataTransferObject;
using MediatR;

namespace EventManagement.Services.EventService
{
    public interface IEventService
    {
        Task<List<EventDto>> GetEventList();
        Task<EventDto> GetEventById(Guid id);
        Task<Guid> CreateEvent(AddEventDto request);
        Task<Unit> EditEvent(Guid id, AddEventDto request);
        Task<Unit> DeleteEvent(Guid id);
    }
}
