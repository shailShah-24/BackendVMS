using Microsoft.EntityFrameworkCore;
using WebApplicationVMS.model;

namespace WebApplicationVMS.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options) {


        }
        public DbSet<login> Login { get; set; }
        public DbSet<VisitorEntity> Visitor { get; set; }

        public DbSet<LogEntity> Log { get; set; }
    }
}
