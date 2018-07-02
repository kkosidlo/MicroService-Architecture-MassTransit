using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleTaskManager.Core.Services;
using SimpleTaskManager.ViewModels;
using System;
using System.Threading.Tasks;

namespace SimpleTaskManager.Pages
{
    public class CreateViewModel : PageModel
    {
        [BindProperty]
        public TaskViewModel Task { get; set; }

        private readonly ITaskManagerService _taskManagerService;

        public CreateViewModel(ITaskManagerService taskManagerService)
        {
            _taskManagerService = taskManagerService ?? throw new ArgumentNullException(nameof(taskManagerService));
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _taskManagerService.AddTaskToDatabase(Task);

            return RedirectToPage("/Index");
        }
    }
}