using Microsoft.EntityFrameworkCore;
using SimpleTaskManager.Core.Model;

namespace SimpleTaskManager.Dal
{
    public class TaskManagerContext : DbContext
    {
        public TaskManagerContext(DbContextOptions<TaskManagerContext> options) : base(options)
        { }

        public virtual DbSet<Assignment> Assignment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AssignmentConfiguration());
        }
    }
}
