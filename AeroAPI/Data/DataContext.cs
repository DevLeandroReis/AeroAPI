using AeroAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AeroAPI.Data
{
     public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Passageiro> Passageiros { get; set; }
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {     
            modelBuilder.Entity<Passageiro>().ToTable("Passageiro");   
  
            modelBuilder.Entity<Passageiro>()
                .HasKey(c => new { c.Id });   
        }
    }
}
