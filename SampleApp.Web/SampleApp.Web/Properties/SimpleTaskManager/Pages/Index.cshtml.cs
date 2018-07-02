using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleTaskManager.Core.Services;
using SimpleTaskManager.ViewModels;

namespace SimpleTaskManager.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<TaskViewModel> TasksList { get; set; }

        private readonly ITaskManagerService _taskManagerService;

        public IndexModel(ITaskManagerService taskManagerService)
        {
            _taskManagerService = taskManagerService ?? throw new ArgumentNullException(nameof(taskManagerService));
        }
        public void OnGet()
        {
            TasksList = _taskManagerService
                .GetOpenTasksFromDatabase()
                .OrderByDescending(x => x.TaskStatus)
                .ToList();
        }
    }
}
