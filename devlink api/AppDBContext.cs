using Microsoft.EntityFrameworkCore;
using devlink_api.Models;




namespace devlink_api
{
    public class AppDBContext: DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
        public DbSet<Link> Links { get; set; }
    }
}
