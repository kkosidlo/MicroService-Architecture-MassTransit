using SimpleTaskManager.Core.Model;
using System.Collections.Generic;

namespace SimpleTaskManager.Core.Repositories
{
    public interface ITaskManagerRepository
    {
        void AddTaskToDatabase(Assignment task);
        Assignment GetTaskById(int? id);
        List<Assignment> GetOpenTasksFromDatabase();
        int GetTotalTasksCountByTaskName(string taskName);
    }
}
