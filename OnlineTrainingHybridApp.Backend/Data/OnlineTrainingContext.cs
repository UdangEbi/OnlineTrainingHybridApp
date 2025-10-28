using Microsoft.EntityFrameworkCore;
using OnlineTrainingHybridApp.Backend.Models;

namespace OnlineTrainingHybridApp.Backend.Data
{
    public class OnlineTrainingContext : DbContext
    {
        public OnlineTrainingContext(DbContextOptions<OnlineTrainingContext> options) : base(options)
        {
        }
        public DbSet<Models.Courses> Courses { get; set; }
        public DbSet<Models.Trainers> Trainers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trainers>()
                .HasMany(t => t.Courses)
                .WithOne(c => c.Trainer)
                .HasForeignKey(c => c.TrainerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
