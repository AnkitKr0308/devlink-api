using devlink_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<IEnumerable<Link>>> GetLinkByUser(string userId)
        {
            var data = await _context.Links.Where(link=>link.UserId == userId).ToListAsync();

            if (data == null) 
            {
                return NotFound("No Data Found");
                    }
            else
            {
                return data;
            }
        }

        [HttpPost]
        public async Task<ActionResult<Link>> PostLink(Link link)
        {
            _context.Links.Add(link);
            await _context.SaveChangesAsync();

            
            return CreatedAtAction(nameof(GetLinkByUser), new { userId = link.UserId }, link);
        }
    }
}
