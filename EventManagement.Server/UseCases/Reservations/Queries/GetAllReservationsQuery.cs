using EventManagement.Server.Core.Entities;
using EventManagement.Shared.DataTransferObject;
using EventManagementApp.Server.Core.Interfaces;
using MediatR;
using MongoDB.Driver;

namespace EventManagement.Server.UseCases.Reservations.Queries
{
    public class GetAllReservationsQuery : IRequest<List<ReservationDto>> 
    {
        public GetAllReservationsQuery() { }
    }

    public class GetAllReservationsQueryHandler : IRequestHandler<GetAllReservationsQuery, List<ReservationDto>>
    {
        private readonly IMongoCollection<Reservation> _reservations;

        public GetAllReservationsQueryHandler(IMongoCollectionFactory collectionFactory)
        {
            _reservations = collectionFactory.GetCollection<Reservation>("Reservations");
        }

        public async Task<List<ReservationDto>> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<Reservation>.Filter.Empty;
            var reservations = await _reservations.Find(filter).Project(ReservationMapping.ReservationProjection).ToListAsync();
            return reservations;
        }
    }
}
