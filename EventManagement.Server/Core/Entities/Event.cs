using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EventManagement.Server.Core.Entities
{
    public class Event
    {
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid VenueId { get; set; }
        public int Capacity { get; set; }
        public int AvailableSeats { get; set; }
    }
}
