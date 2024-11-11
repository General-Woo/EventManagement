using EventManagement.Server.Core.Entities;
using EventManagement.Shared.DataTransferObject;
using EventManagementApp.Server.Core.Interfaces;
using MediatR;
using MongoDB.Driver;

namespace EventManagement.Server.UseCases.Reservations.Commands
{
    public class CreateReservationCommand : IRequest<Guid>
    {
        public CreateReservationCommand(AddReservationDto reservationDto)
        {
            EventId = reservationDto.EventId;
            UserId = reservationDto.UserId;
            NumberOfSeats = reservationDto.NumberOfSeats;
        }

        public Guid EventId { get; set; }
        public Guid UserId { get; set; }
        public int NumberOfSeats { get; set; }
    }

    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, Guid>
    {
        private readonly IMongoCollection<Reservation> _reservations;

        public CreateReservationCommandHandler(IMongoCollectionFactory collectionFactory)
        {
            _reservations = collectionFactory.GetCollection<Reservation>("Reservations");
        }

        public async Task<Guid> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var reservationToAdd = new Reservation
            {
                Id = Guid.NewGuid(),
                EventId = request.EventId,
                UserId = request.UserId,
                ReservationDate  = DateTime.Now,
                NumberOfSeats = request.NumberOfSeats
            };

            await _reservations.InsertOneAsync(reservationToAdd, cancellationToken: cancellationToken);
            return reservationToAdd.Id;
        }
    }
}
