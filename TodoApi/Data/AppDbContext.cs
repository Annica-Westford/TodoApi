using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<TodoModel> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoModel>().HasData(new TodoModel
            {
                Id = 1,
                Task = "Eat Cake",
                IsDone = false,
            }, new TodoModel()
            {
                Id = 2,
                Task = "Drink wine",
                IsDone = false
            }, new TodoModel()
            {
                Id = 3,
                Task = "Vacuum",
                IsDone = true
            }
            );
        }
    }
}
