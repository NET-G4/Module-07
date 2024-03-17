using Lesson02.Models;
using Microsoft.EntityFrameworkCore;

namespace Lesson02.Data
{
    public class TodoDbContext : DbContext
    {
        public virtual DbSet<Todo> Todos { get; set; }
        public virtual DbSet<Models.Task> Tasks { get; set; }
        public TodoDbContext(DbContextOptions<TodoDbContext> options)
            : base(options)
        {
        }
    }
}
