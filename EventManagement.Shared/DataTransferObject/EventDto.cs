namespace EventManagement.Shared.DataTransferObject
{
    public class EventDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid VenueId { get; set; }
        public int Capacity { get; set; }
        public int AvailableSeats { get; set; }
    }
}
