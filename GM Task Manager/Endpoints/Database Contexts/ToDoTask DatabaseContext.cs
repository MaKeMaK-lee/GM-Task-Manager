using GM_Task_Manager.Store.Entities.ToDoTask;
using Microsoft.EntityFrameworkCore;

namespace GM_Task_Manager.Endpoints.Database_Contexts
{
    public class ToDoTask_DatabaseContext : DbContext
    {
        readonly private string _filename;

        public ToDoTask_DatabaseContext(string filename)
        {
            _filename = filename;
        }

        public DbSet<ToDoTask> ToDoTasks { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=" + _filename);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
