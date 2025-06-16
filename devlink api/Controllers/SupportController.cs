using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using devlink_api.Models;
using Microsoft.EntityFrameworkCore;

namespace devlink_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportController : ControllerBase
    {
        private readonly AppDBContext _context;
        public SupportController(AppDBContext context) {
            _context = context;

        }
        [HttpGet("CaseId/{SupportId}")]
        public async Task<ActionResult<IEnumerable<Support>>> GetSupport(ulong supportId)
        {
            var supportData = await _context.Support.Where(support => support.SupportId == supportId).ToListAsync();

            if (supportData == null)
            {
                return NotFound("No Request Found");
            }
            else
            {
                return supportData;
            }
        }

        [HttpPost]
        public async Task <ActionResult<Support>> CreateSupport(Support support)
        {

            
            _context.Support.Add(support);
            await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetSupport), new {SupportId=support.SupportId},support);

        }

        [HttpGet("Email/{Email}")]
        public async Task<ActionResult<IEnumerable<Support>>> GetSupportByEmail(string email)
        {
            var supportData = await _context.Support.Where(support => support.Email == email).ToListAsync();
            if(supportData == null)
            {
                return NotFound("No cases found");
            }
            else
            {
                return supportData;
            }
        }
    }
}
