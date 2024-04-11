using LiL.TimeTracking.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LiL.TimeTracking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeEntryController : ControllerBase
    {
       private TimeTrackingDbContext ctx;

        public TimeEntryController(TimeTrackingDbContext context)
        {
            ctx = context;
        }


        // GET: api/<TimeEntryController>
        [HttpGet]
        [ProducesResponseType<IEnumerable<Resources.TimeEntry>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            //TODO: add logic and parameters for paging
            var response = ctx.TimeEntries.ProjectToType<Resources.TimeEntry>().AsEnumerable();
            return Ok(response);
        }

        // GET api/<TimeEntryController>/5
        [HttpGet("{id}")]
        [ProducesResponseType<Resources.TimeEntry>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)  //parameters map to the template by name
        {
            var dbTimeEntry = await ctx.TimeEntries.FindAsync(id);
            if(dbTimeEntry == null){
                return NotFound();
            }

            var response = dbTimeEntry.Adapt<Resources.TimeEntry>();
            return Ok(response);
        }

        // POST api/<TimeEntryController>
        [HttpPost]
        [ProducesResponseType<Resources.TimeEntry>(StatusCodes.Status201Created)]
        [ProducesResponseType<ObjectResult>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ObjectResult>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] Resources.TimeEntry value)
        {
            if(!ModelState.IsValid){
                return Problem("Invalid TimeEntry resource request", statusCode:StatusCodes.Status400BadRequest);
            }
            try{
                var dbTimeEntry = value.Adapt<Models.TimeEntry>();

                await ctx.TimeEntries.AddAsync(dbTimeEntry);
                await ctx.SaveChangesAsync();

                var response = dbTimeEntry.Adapt<Resources.TimeEntry>();

                return CreatedAtAction(nameof(Get), new {id=response.Id}, response);
            }
            catch(Exception ex){
                return Problem("Problem persisting TimeEntry resource", statusCode:StatusCodes.Status500InternalServerError);
            }
        }

        // PUT api/<TimeEntryController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<Resources.TimeEntry>(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ObjectResult>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ObjectResult>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(Guid id, [FromBody] Resources.TimeEntry value)
        {
            if(!ModelState.IsValid){
                return Problem("Invalid TimeEntry resource request", statusCode:StatusCodes.Status400BadRequest);
            }
            try{
                var dbTimeEntry = value.Adapt<Models.TimeEntry>();

                ctx.Entry<Models.TimeEntry>(dbTimeEntry).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await ctx.SaveChangesAsync();
                return NoContent();

            }
            catch(DbUpdateConcurrencyException dbex){
                var dbTimeEntry = ctx.TimeEntries.Find(id);
                if(dbTimeEntry == null)
                {
                    return NotFound();
                }
                else{
                    return Problem("Problem persisting TimeEntry resource", statusCode:StatusCodes.Status500InternalServerError);
                }
            }
            catch(Exception ex){
                return Problem("Problem persisting TimeEntry resource", statusCode:StatusCodes.Status500InternalServerError);
            }
        }

        // PATCH api/<TimeEntryController>/5
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<Resources.TimeEntry>(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ObjectResult>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ObjectResult>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<Resources.TimeEntry> value)
        {
            if(!ModelState.IsValid){
                return Problem("Invalid TimeEntry resource request", statusCode:StatusCodes.Status400BadRequest);
            }
            try{
                var dbTimeEntry = await ctx.TimeEntries.FindAsync(id);
                if(dbTimeEntry == null){
                    return NotFound();
                }

                var TimeEntry = dbTimeEntry.Adapt<Resources.TimeEntry>();
                //apply the patch changes
                value.ApplyTo(TimeEntry, ModelState);

                var patchedTimeEntry = TimeEntry.Adapt<Models.TimeEntry>();
                ctx.Entry<Models.TimeEntry>(dbTimeEntry).CurrentValues.SetValues(patchedTimeEntry);

                await ctx.SaveChangesAsync();
                return NoContent();

            }
            catch(Exception ex){
                return Problem("Problem persisting TimeEntry resource", statusCode:StatusCodes.Status500InternalServerError);
            }
        }

        // DELETE api/<TimeEntryController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType<Resources.TimeEntry>(StatusCodes.Status204NoContent)]
        [ProducesResponseType<ObjectResult>(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var dbTimeEntry = await ctx.TimeEntries.FindAsync(id);
            if(dbTimeEntry == null)
            {
                return NotFound();
            }

            try{
                ctx.TimeEntries.Remove(dbTimeEntry);
                await ctx.SaveChangesAsync();
                return NoContent();
            }
            catch(Exception ex){
                return Problem("Problem deleting TimeEntry resource", statusCode:StatusCodes.Status500InternalServerError);
            }
        }

    }
}
