using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Opuspac.TechnicalTest.Domain
{
    public class User : BaseEntity
    {
        [BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string Email { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string HashedPassword { get; set; }
    }
}
