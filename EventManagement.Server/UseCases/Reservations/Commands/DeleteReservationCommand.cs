using EventManagement.Server.Core.Entities;
using EventManagementApp.Server.Core.Interfaces;
using MediatR;
using MongoDB.Driver;

namespace EventManagement.Server.UseCases.Reservations.Commands
{
    public class DeleteReservationCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }

    public class DeleteReservationCommandHandler : IRequestHandler<DeleteReservationCommand, Unit>
    {
        private readonly IMongoCollection<Reservation> _reservations;

        public DeleteReservationCommandHandler(IMongoCollectionFactory collectionFactory)
        {
            _reservations = collectionFactory.GetCollection<Reservation>("Reservations");
        }

        public async Task<Unit> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            var reservationFilter = Builders<Reservation>.Filter.Eq(e => e.Id, request.Id);
            await _reservations.DeleteOneAsync(reservationFilter, cancellationToken);
            return Unit.Value;
        }
    }
}
