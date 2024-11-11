using EventManagement.Server.Core.Entities;
using EventManagement.Shared.DataTransferObject;
using EventManagementApp.Server.Core.Interfaces;
using MediatR;
using MongoDB.Driver;

namespace EventManagement.Server.UseCases.Reservations.Commands
{
    public class EditReservationCommand : IRequest<Unit>
    {
        public EditReservationCommand(AddReservationDto reservationDto)
        {
            EventId = reservationDto.EventId;
            UserId = reservationDto.UserId;
            NumberOfSeats = reservationDto.NumberOfSeats;
        }
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public Guid UserId { get; set; }
        public int NumberOfSeats { get; set; }
    }

    public class EditReservationCommandHandler : IRequestHandler<EditReservationCommand, Unit>
    {
        private readonly IMongoCollection<Reservation> _reservations;

        public EditReservationCommandHandler(IMongoCollectionFactory collectionFactory)
        {
            _reservations = collectionFactory.GetCollection<Reservation>("Reservations");
        }

        public async Task<Unit> Handle(EditReservationCommand request, CancellationToken cancellationToken)
        {
            var reservationFilter = Builders<Reservation>.Filter.Eq(e => e.Id, request.Id);
            var update = Builders<Reservation>.Update;
            var updateDefinitions = new List<UpdateDefinition<Reservation>>();

            if (!string.IsNullOrEmpty(request.EventId.ToString())) updateDefinitions.Add(update.Set(e => e.EventId, request.EventId));
            if (!string.IsNullOrEmpty(request.UserId.ToString())) updateDefinitions.Add(update.Set(e => e.UserId, request.UserId));
            updateDefinitions.Add(update.Set(e => e.ReservationDate, DateTime.Now));
            if (!string.IsNullOrEmpty(request.NumberOfSeats.ToString())) updateDefinitions.Add(update.Set(e => e.NumberOfSeats, request.NumberOfSeats));

            var combinedUpdateDefinition = update.Combine(updateDefinitions);

            await _reservations.UpdateOneAsync(reservationFilter, combinedUpdateDefinition);
            return Unit.Value;
        }
    }
}
