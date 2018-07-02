using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleTaskManager.Core.Repositories;
using SimpleTaskManager.Dal;
using SimpleTaskManager.Repositories;

namespace SimpleTaskManager.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskManagerContext _ctx;

        public UnitOfWork(TaskManagerContext ctx)
        {
            _ctx = ctx ?? throw new ArgumentNullException(nameof(ctx));

            Assignment = new TaskManagerRepository(ctx);
        }

        public ITaskManagerRepository Assignment { get; }

        public int Complete()
        {
            return _ctx.SaveChanges();
        }
        public void Dispose()
        {
            _ctx.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
