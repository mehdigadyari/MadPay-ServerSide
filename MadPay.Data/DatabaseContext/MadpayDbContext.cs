using MadPay.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MadPay.Data.DatabaseContext
{
   public class MadpayDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlServer(@"Data Source=MEHDI-PC\SQLEXPRESS;Initial Catalog=MadPay724db;Integrated Security=True;MultipleActiveResultSets=True;");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<BankCard> BankCards { get; set; }
    }
}
