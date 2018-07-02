using SimpleTaskManager.Core.Model;
using SimpleTaskManager.Core.Repositories;
using SimpleTaskManager.Dal;
using SimpleTaskManager.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleTaskManager.Repositories
{
    public class TaskManagerRepository : ITaskManagerRepository
    {
        private readonly TaskManagerContext _taskManagerContext;

        public TaskManagerRepository(TaskManagerContext taskManagerContext)
        {
            _taskManagerContext = taskManagerContext ?? throw new ArgumentNullException(nameof(taskManagerContext));
        }
        public void AddTaskToDatabase(Assignment task)
        {
            _taskManagerContext.Add(task);
        }

        public Assignment GetTaskById(int? id)
        {
            return _taskManagerContext
                .Assignment
                .FirstOrDefault(x => x.Id == id);
        }

        public int GetTotalTasksCountByTaskName(string taskName)
        {
            return _taskManagerContext
                .Assignment
                .Select(x => x.TaskName.Equals(taskName))
                .Count();
        }

        public List<Assignment> GetOpenTasksFromDatabase()
        {
            return _taskManagerContext
                .Assignment
                .ToList();
        }
    }
}
