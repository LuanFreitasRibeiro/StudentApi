using Microsoft.EntityFrameworkCore;
using StudentApi.Context.Map;
using StudentApi.Models;

namespace StudentApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new StudentMap());
        }
    }
}
