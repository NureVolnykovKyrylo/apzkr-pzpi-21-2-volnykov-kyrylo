using AquaTrack.Models;
using Microsoft.EntityFrameworkCore;

namespace AquaTrack.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Aquarium> Aquariums { get; set; }
        public DbSet<Inhabitant> Inhabitants { get; set; }
        public DbSet<FeedingSchedule> FeedingSchedules { get; set; }
        public DbSet<ResearchReport> ResearchReports { get; set; }
        public DbSet<SensorData> SensorData { get; set; }
        public DbSet<AnalysisReport> AnalysisReports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ResearchReport>()
                .HasOne(a => a.AnalysisReport)
                .WithOne(b => b.ResearchReport)
                .HasForeignKey<AnalysisReport>(b => b.ResearchReportId);
        }
    }
}
