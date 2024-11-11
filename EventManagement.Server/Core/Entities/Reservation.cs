using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EventManagement.Server.Core.Entities
{
    public class Reservation
    {
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid EventId { get; set; }
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid UserId { get; set; }
        public DateTime ReservationDate { get; set; }
        public int NumberOfSeats { get; set; }
    }
}
