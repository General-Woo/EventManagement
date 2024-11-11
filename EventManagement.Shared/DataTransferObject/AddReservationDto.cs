namespace EventManagement.Shared.DataTransferObject
{
    public class AddReservationDto
    {
        public Guid EventId { get; set; }
        public Guid UserId { get; set; }
        public int NumberOfSeats { get; set; }
    }
}
