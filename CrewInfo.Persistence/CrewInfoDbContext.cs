

using CrewInfo.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrewInfo.Persistence
{
    public class CrewInfoDbContext(DbContextOptions <CrewInfoDbContext> options) : DbContext(options)
    {
        public DbSet<PilotEntity> Pilots { get; set; }
        public DbSet<StewardEntity> Stewards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CrewInfoDbContext).Assembly);
        }
    }
}
