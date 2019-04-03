using Madyan.Data.Context;
using Madyan.Repo.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
namespace Madyan.Data.Exception
{
    public class ErrorDetails: IEntityBase
    {
        public DateTime ErrorDate { get; set; }
        public long ErrorID { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }

        
    }
   
    public partial class ExceptionDBContext : DbContext, IExceptionDBContext
    {
        public DbSet<ErrorDetails> EntBasic { get; set; }
        public ExceptionDBContext(DbContextOptions<ExceptionDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region model
            modelBuilder.Entity<ErrorDetails>().ToTable("ErrorDetails");
            modelBuilder.Entity<ErrorDetails>().HasKey(p => p.ErrorID);
            modelBuilder.Entity<ErrorDetails>().Property(p => p.ErrorID).ValueGeneratedOnAdd();
            #endregion  model

        }
    }
}
