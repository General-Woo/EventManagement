using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EventManagement.Server.Core.Entities
{
    public class User
    {
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
