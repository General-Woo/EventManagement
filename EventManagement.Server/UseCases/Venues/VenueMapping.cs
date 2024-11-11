using EventManagement.Server.Core.Entities;
using EventManagement.Shared.DataTransferObject;
using System.Linq.Expressions;

namespace EventManagement.Server.UseCases.Venues
{
    public static class VenueMapping
    {
        public static Expression<Func<Venue, VenueDto>> VenueProjection
        {
            get
            {
                return d => new VenueDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Address = d.Address,
                    Capacity = d.Capacity
                };
            }
        }
    }
}