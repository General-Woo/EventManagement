using EventManagement.Shared.DataTransferObject;
using MediatR;

namespace EventManagement.Services.VenueService
{
    public interface IVenueService
    {
        Task<List<VenueDto>> GetVenueList();
        Task<VenueDto> GetVenueById(Guid id);
        Task<Guid> CreateVenue(AddVenueDto request);
        Task<Unit> EditVenue(Guid id, AddVenueDto request);
        Task<Unit> DeleteVenue(Guid id);
    }
}
