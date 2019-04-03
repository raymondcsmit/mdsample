using Madyan.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Madyan.Data
{
    
    public partial class DDBDBContext : DbContext, IDDBDBContext
    {
        public DbSet<Basic> EntBasic { get; set; }
        public DbSet<NextOfKin> EntNextOfKin { get; set; }
        public DbSet<GpDetail> EntGpDetails { get; set; }
        public DbSet<Patient> EntPatient { get; set; }
        public DDBDBContext(DbContextOptions<DDBDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region model
            modelBuilder.Entity<Basic>().ToTable("Basic");
            modelBuilder.Entity<Basic>().HasKey(p => p.BasicID);
            modelBuilder.Entity<Basic>().Property(p => p.BasicID).ValueGeneratedOnAdd();

            modelBuilder.Entity<NextOfKin>().ToTable("NextOfKin");
            modelBuilder.Entity<NextOfKin>().HasKey(p => p.NextOfKinID);
            modelBuilder.Entity<NextOfKin>().Property(p => p.NextOfKinID).ValueGeneratedOnAdd();
           
            modelBuilder.Entity<Patient>().ToTable("Patient");
            modelBuilder.Entity<Patient>().HasKey(p => p.PatientID);
            modelBuilder.Entity<Patient>().Property(p => p.PatientID).ValueGeneratedOnAdd();
            

            modelBuilder.Entity<Patient>().HasOne<NextOfKin>(p => p.FkNextOfKin)
           .WithOne(nfk => nfk.FkPatient).HasForeignKey<NextOfKin>(n => n.FkPatientID).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Patient>().HasOne<Basic>(p => p.FkBasic)
          .WithOne(nfk => nfk.FkPatient).HasForeignKey<Basic>(n => n.FkPatientID).OnDelete(DeleteBehavior.Cascade);

        


            modelBuilder.Entity<GpDetail>().ToTable("GpDetail");
            modelBuilder.Entity<GpDetail>().HasKey(p => p.GpDetailID);
            modelBuilder.Entity<GpDetail>().Property(p => p.GpDetailID).ValueGeneratedOnAdd();
            modelBuilder.Entity<GpDetail>().HasOne(t => t.FkPatient).WithMany(t => t.GpDetailList)
                .HasForeignKey(d => d.FkPatientID).OnDelete(DeleteBehavior.Cascade); ;
            #endregion  model
            
        }

        
    }
}
