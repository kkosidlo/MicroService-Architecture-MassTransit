using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SimpleTaskManager.Core.Services;
using SimpleTaskManager.ViewModels;
using System;

namespace SimpleTaskManager.Pages
{
    public class EditPageModel : PageModel
    {
        [BindProperty]
        public EditTaskViewModel Task { get; set; }

        private readonly ITaskManagerService _taskManagerService;

        public EditPageModel(ITaskManagerService taskManagerService)
        {
            _taskManagerService = taskManagerService ?? throw new ArgumentNullException(nameof(taskManagerService));
        }

        public IActionResult OnGet(int? id)
        {
            if (id == null)
                return NotFound();

            Task = _taskManagerService.GetTaskById(id);

            if (Task == null)
                return NotFound();

            return Page();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _taskManagerService.EditTask(Task);

            return RedirectToPage("/Index");
        }
    }
}