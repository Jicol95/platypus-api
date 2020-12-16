using Microsoft.EntityFrameworkCore;
using Platypus.Model.Entity;

namespace Platypus.Data.Context {

    public class DataContext : DbContext {

        public DataContext(DbContextOptions<DataContext> options) : base(options) {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserToken> UserTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User>().HasIndex(p => p.EmailAddress).IsUnique();
            modelBuilder.Entity<User>().HasIndex(p => p.Firstname);
            modelBuilder.Entity<User>().HasIndex(p => p.Lastname);

            modelBuilder.Entity<UserToken>().HasIndex(p => p.UserId);
            modelBuilder.Entity<UserToken>().HasIndex(p => p.Token);
        }
    }
}