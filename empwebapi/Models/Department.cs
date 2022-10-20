using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace empwebapi.Models
{
    public class Department
    {
        

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("DepartmentId")]
        public int DepartmentId { get; set; }

        [BsonElement("DepartmentName")]
        public string ? DepartmentName { get; set; }
        
    }
}


