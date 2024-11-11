using EventManagement.Server.Core.Entities;
using EventManagement.Shared.DataTransferObject;
using System.Linq.Expressions;

namespace EventManagement.Server.UseCases.Events
{
    public static class EventMapping
    {
        public static Expression<Func<Event, EventDto>> EventProjection
        {
            get
            {
                return d => new EventDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    StartDate = d.StartDate,
                    EndDate = d.EndDate,
                    VenueId = d.VenueId,
                    Capacity = d.Capacity,
                    AvailableSeats = d.AvailableSeats
                };
            }
        }
    }
}