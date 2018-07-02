using SimpleTaskManager.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleTaskManager.ViewModels
{
    public class EditTaskViewModel
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public DateTime LastModified { get; set; }
        public AssignmentStatus Status { get; set; }
    }
}
