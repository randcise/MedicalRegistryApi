using Microsoft.EntityFrameworkCore;

namespace MedicalRegistryApi.Models
{
    public class MedicalDbContext : DbContext
    {
        public MedicalDbContext(DbContextOptions<MedicalDbContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients => Set<Patient>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    Id = 1,
                    FullName = "Олександр Петренко",
                    Diagnosis = "Грип",
                    Treatment = "Противірусні препарати"
                },

                new Patient
                {
                    Id = 2,
                    FullName = "Марія Коваленко",
                    Diagnosis = "Бронхіт",
                    Treatment = "Антибіотики"
                }
            );
        }
    }
}