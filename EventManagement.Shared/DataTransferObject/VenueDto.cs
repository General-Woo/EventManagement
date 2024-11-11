namespace EventManagement.Shared.DataTransferObject
{
    public class VenueDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Capacity { get; set; }
    }
}
