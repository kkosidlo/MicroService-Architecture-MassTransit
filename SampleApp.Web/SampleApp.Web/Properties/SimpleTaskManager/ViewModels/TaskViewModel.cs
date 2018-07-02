using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleTaskManager.ViewModels
{
    public class TaskViewModel
    {
        public int Id { get; set; }
        public string TaskStatus { get; set; }
        [Required]
        public string TaskName { get; set; }
        public int NrOfTimesTaskCreated { get; set; }
        public int NrOfTimesTaskUpdated { get; set; }
        public DateTime LastModified { get; set; }
    }
}
