using LiL.TimeTracking.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Converters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LiL.TimeTracking.Controllers
{
    [Route("")] // 
    [ApiController]
    public class HomeController : ControllerBase
    {
        // GET: api/<EmployeeController>
        [HttpGet]
        [ProducesResponseType<IEnumerable<Resources.Resource>>(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var resources = new List<Resources.Resource>{
                new Resources.Resource("Employees", "/api/Employee"),
                new Resources.Resource("Projects", "/api/Project"),
                new Resources.Resource("Time Entries", "/api/TimeEntry"),
                new Resources.Resource("Project Assignments", "/api/ProjectAssignment")
            };

            return Ok(resources);
        }

        
    }
}
