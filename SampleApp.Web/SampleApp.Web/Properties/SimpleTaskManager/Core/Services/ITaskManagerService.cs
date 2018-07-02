using SimpleTaskManager.ViewModels;
using System.Collections.Generic;

namespace SimpleTaskManager.Core.Services
{
    public interface ITaskManagerService
    {
        List<TaskViewModel> GetOpenTasksFromDatabase();
        void AddTaskToDatabase(TaskViewModel task);
        void EditTask(EditTaskViewModel task);
        EditTaskViewModel GetTaskById(int? id);
    }
}
