using empwebapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;



namespace empwebapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public EmployeeController(IConfiguration configuration, IWebHostEnvironment env)
        {
            this._configuration = configuration;
            _env = env;
        }
         

        [HttpGet]
        public JsonResult Get()
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("EmployeeAppCon"));

            var dbList = dbClient.GetDatabase("empdb").GetCollection<Employee>("Employee").AsQueryable();

            return new JsonResult(dbList);
        }

        [HttpPost]
        public JsonResult Post(Employee emp)
        {

            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("EmployeeAppCon"));

            int LastEmployeeId = dbClient.GetDatabase("empdb").GetCollection<Employee>("Employee").AsQueryable().Count();
            emp.EmployeeId = LastEmployeeId + 1;

            dbClient.GetDatabase("empdb").GetCollection<Employee>("Employee").InsertOne(emp);

            return new JsonResult("Employee Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Employee emp)
        {

            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("EmployeeAppCon"));

            var filter = Builders<Employee>.Filter.Eq("EmployeeId", emp.EmployeeId);

            var update = Builders<Employee>.Update.Set("EmployeeName", emp.EmployeeName)
                                                    .Set("Department", emp.Department)
                                                    .Set("EmployeeEmail", emp.EmployeeEmail)
                                                    .Set("EmployeePhone", emp.EmployeePhone)
                                                    .Set("EmployeeSalary", emp.EmployeeSalary)
                                                    .Set("DateoOfJoining", emp.DateoOfJoining)
                                                    .Set("PhotoFileName", emp.PhotoFileName);
                                                   


            dbClient.GetDatabase("empdb").GetCollection<Employee>("Employee").UpdateOne(filter, update);

            return new JsonResult("Employee Updated Successfully");
        }


        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {

            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("EmployeeAppCon"));
            var filter = Builders<Employee>.Filter.Eq("EmployeeId", id);


            dbClient.GetDatabase("empdb").GetCollection<Employee>("Employee").DeleteOne(filter);

            return new JsonResult("Employee Deleted Successfully");
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos" + filename;

                using(var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {
                return new JsonResult("anonymous.png");
            };
        }

    }
}
