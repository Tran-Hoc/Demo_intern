using Microsoft.EntityFrameworkCore;

namespace Demo_intern.Data
{
    public class DemoDbContext: DbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options) { }

        public DbSet<TimeValue> TimeValues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TimeValue>(
                e =>
                {
                    e.ToTable("TimeValue");
                    e.HasKey(e => e.Id);
                });
      
        }
    }
}
