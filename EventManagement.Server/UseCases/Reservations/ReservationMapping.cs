using EventManagement.Server.Core.Entities;
using EventManagement.Shared.DataTransferObject;
using System.Linq.Expressions;

namespace EventManagement.Server.UseCases.Reservations
{
    public static class ReservationMapping
    {
        public static Expression<Func<Reservation, ReservationDto>> ReservationProjection
        {
            get
            {
                return d => new ReservationDto
                {
                    Id = d.Id,
                    EventId = d.EventId,
                    UserId = d.UserId,
                    ReservationDate = d.ReservationDate,
                    NumberOfSeats = d.NumberOfSeats
                };
            }
        }
    }
}