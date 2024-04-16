using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Model.PacientModel;
using Microsoft.EntityFrameworkCore;

namespace Anamnese.API.ORM.Context
{
    public class AnamneseDbContext : DbContext
    {
        public AnamneseDbContext(DbContextOptions<AnamneseDbContext> options) : base(options)
        {
        }

        public DbSet<ProfissionalModel> Profissional { get; set; }
        public DbSet<PacientModel> Pacient { get; set; }
        public DbSet<ReportModel> Report { get; set; }
        public DbSet<ReferralModel> Referral { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PacientModel>()
                .HasOne(p => p.Report)
                .WithOne(r => r.Pacient)
                .HasForeignKey<ReportModel>(r => r.PacientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReferralModel>()
                .HasOne(r => r.Pacient)
                .WithMany(p => p.Referrals)
                .HasForeignKey(r => r.PacientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
