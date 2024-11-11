using EventManagement.Server.Core.Entities;
using EventManagement.Shared.DataTransferObject;
using EventManagementApp.Server.Core.Interfaces;
using MediatR;
using MongoDB.Driver;

namespace EventManagement.Server.UseCases.Reservations.Queries
{
    public class GetReservationByIdQuery : IRequest<ReservationDto>
    {
        public Guid Id { get; set; }

        public GetReservationByIdQuery(Guid id)
        {
            Id = id;
        }
    }

    public class GetReservationByIdQueryHandler : IRequestHandler<GetReservationByIdQuery, ReservationDto>
    {
        private readonly IMongoCollection<Reservation> _reservations;

        public GetReservationByIdQueryHandler(IMongoCollectionFactory collectionFactory)
        {
            _reservations = collectionFactory.GetCollection<Reservation>("Reservations");
        }

        public async Task<ReservationDto> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
        {
            var filter = Builders<Reservation>.Filter.Eq(r => r.Id, request.Id);
            var reservation = await _reservations.Find(filter).Project(ReservationMapping.ReservationProjection).FirstOrDefaultAsync();
            return reservation;
        }
    }
}
