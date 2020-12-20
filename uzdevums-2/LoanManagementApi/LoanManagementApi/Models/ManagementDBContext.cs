using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanManagementApi.Models
{
    public class ManagementDBContext : DbContext
    {
        public ManagementDBContext(DbContextOptions<ManagementDBContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Loan> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Loan>()
                .Property(l => l.Amount)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Loan>()
                .HasOne(l => l.Lender)
                .WithMany(l => l.GivenLoans)
                .HasForeignKey(l => l.LenderId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Loan>()
                .HasOne(l => l.Borrower)
                .WithMany(l => l.TakenLoans)
                .HasForeignKey(l => l.BorrowerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
