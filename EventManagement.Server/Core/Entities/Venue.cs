using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EventManagement.Server.Core.Entities
{
    public class Venue
    {
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Capacity { get; set; }
    }
}
