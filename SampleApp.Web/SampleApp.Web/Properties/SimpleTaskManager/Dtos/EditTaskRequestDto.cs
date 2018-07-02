using SimpleTaskManager.Enums;
using System;

namespace SimpleTaskManager.Dtos
{
    public class EditTaskRequestDto
    {
        public string OldTaskName { get; set; }
        public string NewTaskName { get; set; }
        public AssignmentStatus TaskStatus { get; set; }
    }
}
