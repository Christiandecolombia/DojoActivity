using Microsoft.EntityFrameworkCore;

namespace BeltExam.Models
{
    public class BeltExamContext : DbContext
    {
        public BeltExamContext (DbContextOptions<BeltExamContext> options) : base(options) { }
        public DbSet<User> User { get; set; }
        public DbSet<Activity> Activity { get; set; }
        public DbSet<Participant> Participant { get; set; }
    }
}