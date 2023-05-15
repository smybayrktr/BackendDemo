using System;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntityFramework
{
	public class DemoContext: DbContext
	{
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=Deneme;User ID=SA;Password=reallyStrongPwd123;Encrypt=True;TrustServerCertificate=true");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

    }
}

