using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RestaurantWebApp.Models
{
    public class TransactionsContext : DbContext
    {
        public TransactionsContext(DbContextOptions<TransactionsContext> options)
            : base(options)
        {

        }

        public DbSet<Transactions> Transactions { get; set; }

        // Method di bawah digunakan untuk menambahkan relasi dengan tabel customer dan food
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transactions>()
                .HasOne(t => t.Customer)
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.customerId);

            modelBuilder.Entity<Transactions>()
                .HasOne(t => t.Food)
                .WithMany(f => f.Transactions)
                .HasForeignKey(t => t.foodId);
        }
    }
}