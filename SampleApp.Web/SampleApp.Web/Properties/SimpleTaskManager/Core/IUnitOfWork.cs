using SimpleTaskManager.Core.Repositories;
using System;

namespace SimpleTaskManager.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ITaskManagerRepository Assignment { get; }

        int Complete();
    }
}
