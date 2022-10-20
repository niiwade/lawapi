using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace empwebapi.Models
{

  
    public class Employee
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }


        [BsonElement("DateOfJoining")]
        public string? DateoOfJoining { get; set; }


        [BsonElement("Department")]
        public string? Department { get; set; }

        [BsonElement("EmployeeId")]
        public int EmployeeId { get; set; }

        [BsonElement("EmployeeName")]
        public string? EmployeeName { get; set; }

        [BsonElement("EmployeeEmail")]
        public string? EmployeeEmail { get; set; }

        [BsonElement("EmployeePhone")]
        public string? EmployeePhone { get; set; }

        [BsonElement("EmployeeSalary")]
        public string? EmployeeSalary { get; set; }

        [BsonElement("PhotoFileName")]
        public string? PhotoFileName { get; set; }



    }
}
