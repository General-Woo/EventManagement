namespace EventManagement.Shared.DataTransferObject
{
    public class ReservationDto
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public Guid UserId { get; set; }
        public DateTime ReservationDate { get; set; }
        public int NumberOfSeats { get; set; }
    }
}
