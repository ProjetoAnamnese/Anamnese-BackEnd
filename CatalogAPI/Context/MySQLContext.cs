using CatalogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options)
        {
        }
        
        public DbSet<DoctorModel> Doctor { get; set; }
        public DbSet<PacientModel> Pacient { get; set; }

        //Atua como FluentAPI
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            #region user
            //modelBuilder.Entity<DoctorModel>().HasKey(u => u.Id);
            //modelBuilder.Entity<DoctorModel>().Property(u => u.UserName).HasMaxLength(255).IsRequired();
            //modelBuilder.Entity<DoctorModel>().Property(u => u.Email).HasMaxLength(255).IsRequired();
            //modelBuilder.Entity<DoctorModel>().Property(u => u.Password).HasMaxLength(255).IsRequired();
            //modelBuilder.Entity<UserModel>().HasMany(u => u.Patients).WithOne(p => p.Doctor);
            #endregion user

            //#region pacient
            //modelBuilder.Entity<PacientModel>().HasKey(u => u.Id);
            //modelBuilder.Entity<PacientModel>().Property(u => u.UserName).HasMaxLength(255).IsRequired();
            //modelBuilder.Entity<PacientModel>().HasOne(p => p.Doctor).WithMany(u => u.Patients).HasForeignKey(p => p.DoctorId);
            //modelBuilder.Entity<PacientModel>().Property(p => p.DoctorId).ValueGeneratedNever();
            //#endregion pacient

        }
    }
}
