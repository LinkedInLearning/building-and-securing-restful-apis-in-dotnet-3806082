using LiL.TimeTracking.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LiL.TimeTracking.Controllers
{
    [Route("api/[controller]")] // /api/Employee
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private TimeTrackingDbContext ctx;

        public EmployeeController(TimeTrackingDbContext context)
        {
            ctx = context;
        }


        // GET: api/<EmployeeController>
        [HttpGet]
        [ProducesResponseType<IEnumerable<Resources.Employee>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            //TODO: add logic and parameters for paging
            var response = ctx.Employees.ProjectToType<Resources.Employee>().AsEnumerable();
            return Ok(response);
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        [ProducesResponseType<Resources.Employee>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)  //parameters map to the template by name
        {
            var dbEmployee = await ctx.Employees.FindAsync(id);
            if(dbEmployee == null){
                return NotFound();
            }

            var response = dbEmployee.Adapt<Resources.Employee>();
            return Ok(response);
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
