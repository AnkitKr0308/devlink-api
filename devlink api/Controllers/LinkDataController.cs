using devlink_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;


namespace devlink_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinkDataController : ControllerBase
    {
        private readonly AppDBContext _context;
        public LinkDataController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/links
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Link>>> GetLinks()
        {
            return await _context.Links.ToListAsync();
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<Link>>> GetLinkByUser(string userId, [FromQuery] string? search)
        {
            
            var userParam = new MySqlParameter("@userIdParam", userId);
            var searchParam = new MySqlParameter("@SearchParam", search ?? (object)DBNull.Value);

            var results = await _context.Links.
                   FromSqlRaw("CALL sp_GetFilteredLinks(@userIdParam, @SearchParam)", userParam, searchParam)
                   .ToListAsync();

            if (results == null) 
            {
                return NotFound("No Data Found");
                    }
            return results;
        }

        [HttpPost]
        public async Task<ActionResult<Link>> PostLink(Link link)
        {
            _context.Links.Add(link);
            await _context.SaveChangesAsync();

            
            return CreatedAtAction(nameof(GetLinkByUser), new { userId = link.UserId }, link);
        }

        [HttpDelete("{id}")]
        public async Task <IActionResult> DeleteLink (int id)
        {
           var delete =  await _context.Links.Where(Link => Link.id == id).ExecuteDeleteAsync();

           if(delete == 0)
            {
                return NotFound();
            }
            return Ok("ID: "+id +" deleted successfully");
        }

        [HttpPut("{id}")]
        public async Task <ActionResult<Link>> UpdateLink (int id, [FromBody] Link updatedLink)
        {
            var existingLink = await _context.Links.FirstOrDefaultAsync(l => l.id == id);
            if(existingLink == null)
            {
                return NotFound();
            }
            existingLink.Title = updatedLink.Title;
            existingLink.Description = updatedLink.Description;
            existingLink.Url = updatedLink.Url;
            existingLink.Category = updatedLink.Category;

            await _context.SaveChangesAsync();

            return Ok(existingLink);
        }
    }
}
