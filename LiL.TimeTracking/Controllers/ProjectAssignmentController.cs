using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LiL.TimeTracking.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using LiL.TimeTracking.Resources;

namespace LiL.TimeTracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectAssignmentController : ControllerBase
    {
       private TimeTrackingDbContext ctx;

        public ProjectAssignmentController(TimeTrackingDbContext context)
        {
            ctx = context;
        }


        // GET: api/<ProjectAssignmentController>
        [HttpGet]
        [ProducesResponseType<IEnumerable<Resources.ProjectAssignment>>(StatusCodes.Status200OK)]
        public async IAsyncEnumerable<Resources.ProjectAssignment> Get()
        {
            //TODO: add logic and parameters for paging
            var dbProjects = ctx.Projects.Include(p=>p.Members).AsAsyncEnumerable();
            await foreach(var p in dbProjects){
                foreach(var e in p.Members){
                    yield return new Resources.ProjectAssignment(e.Id, p.Id, e.Name, p.Name);
                }
            }
        }

        // POST api/<ProjectAssignmentController>
        [HttpPost]
        [ProducesResponseType<Resources.ProjectAssignment>(StatusCodes.Status201Created)]
        [ProducesResponseType<ObjectResult>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ObjectResult>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] Resources.ProjectAssignment value)
        {
            if(!ModelState.IsValid){
                return Problem("Invalid Project Assignment resource request", statusCode:StatusCodes.Status400BadRequest);
            }
            try{
                var dbEmployee = await ctx.Employees.FindAsync(value.EmployeeId);
                var dbProject = await ctx.Projects.FindAsync(value.ProjectId);

                if(dbEmployee == null || dbProject == null){
                    return Problem("Either employee or project was not found");
                }
                dbEmployee.Projects = new HashSet<Models.Project>();
                dbEmployee.Projects.Add(dbProject);

                await ctx.SaveChangesAsync();

                
                return Created();
            }
            catch(Exception ex){
                return Problem("Problem persisting ProjectAssignment resource", statusCode:StatusCodes.Status500InternalServerError);
            }
        }

        

        // DELETE api/<ProjectAssignmentController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<Resources.ProjectAssignment>(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ObjectResult>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromBody] ProjectAssignment value)
        {
           var dbEmployee = await ctx.Employees.FindAsync(value.EmployeeId);
                var dbProject = await ctx.Projects.FindAsync(value.ProjectId);

                if(dbEmployee == null || dbProject == null){
                    return NotFound();
                }

            try{
                dbEmployee.Projects.Remove(dbProject);
                await ctx.SaveChangesAsync();
                return NoContent();
            }
            catch(Exception ex){
                return Problem("Problem deleting ProjectAssignment resource", statusCode:StatusCodes.Status500InternalServerError);
            }
        }


    }
}
