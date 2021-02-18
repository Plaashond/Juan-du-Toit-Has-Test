using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Net;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using AzureFunctions.Extensions.Swashbuckle;

namespace Juan_du_Toit_Has_Test
{
    public class TestFunctions
    {
        private readonly PracticalTestContext _context;
        private readonly ICustomLogger _customLogger;
        public TestFunctions(PracticalTestContext context, ICustomLogger customLogger)
        {
            _context = context;
            _customLogger = customLogger;
        }
        [FunctionName("GetEmployeeList")]
        public IActionResult GetEmployeeList(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var employees = _context.Employees.ToList();
            var persons = _context.Persons.ToList();
            var responseModel = new { employees,persons };
            var response = JsonConvert.SerializeObject(responseModel, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            try
            {
                throw new Exception("something went wrong here");
            }
            catch (Exception ex)
            {
                _customLogger.LogIt(ex);
            }

            return new OkObjectResult(response);
        }

        [FunctionName("GetByEmployeeNumber")]
        public IActionResult GetByEmployeeNumber(
           [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
           ILogger log)
        {
            string employeeNumber = req.Query["EmployeeNumber"];
            var employeeRecords = _context.Employees.FirstOrDefault(x => employeeNumber.Contains(x.EmployeeNo));
            var response = JsonConvert.SerializeObject(employeeRecords, Formatting.Indented,
                new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            try
            {
                throw new Exception("something went wrong there");
            }
            catch (Exception ex)
            {
                _customLogger.LogIt(ex);
            }
            return new OkObjectResult(response);
        }
        [FunctionName("GetFromUrl")]
        public IActionResult Run(
           [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
           ILogger log)
        {
            HttpClient http = new HttpClient();
            string id = req.Query["Id"] ;
            id = string.IsNullOrEmpty(id) ?"1":id;
            var data = http.GetAsync($"https://jsonplaceholder.typicode.com/todos/{id}").Result.Content.ReadAsStringAsync().Result;
            try
            {
                throw new Exception("something went wrong everywhere");
            }
            catch (Exception ex)
            {
                _customLogger.LogIt(ex);
            }
            return new OkObjectResult(data);
        }
        [SwaggerIgnore]
        [FunctionName("swagger")]
        public static Task<HttpResponseMessage> Swagger([HttpTrigger(AuthorizationLevel.Function, "get", Route = "swagger/json")] HttpRequestMessage req,
           [SwashBuckleClient] ISwashBuckleClient swashBuckleClient)
        {
            return Task.FromResult(swashBuckleClient.CreateSwaggerDocumentResponse(req));
        }
        [SwaggerIgnore]
        [FunctionName("swaggerui")]
        public static Task<HttpResponseMessage> SwaggerUI([HttpTrigger(AuthorizationLevel.Function, "get", Route = "swagger/ui")] HttpRequestMessage req,
           [SwashBuckleClient] ISwashBuckleClient swashBuckleClient)
        {
            return Task.FromResult(swashBuckleClient.CreateSwaggerUIResponse(req,documentRoute:"swagger/json"));
        }



    }
}
