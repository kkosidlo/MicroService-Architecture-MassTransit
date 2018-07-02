using AutoMapper;
using Newtonsoft.Json;
using SimpleTaskManager.Core;
using SimpleTaskManager.Core.Model;
using SimpleTaskManager.Core.Services;
using SimpleTaskManager.Dtos;
using SimpleTaskManager.Enums;
using SimpleTaskManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace SimpleTaskManager.Services
{
    public class TaskManagerService : ITaskManagerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRequestService _requestService;

        private const string ValidateTaskNameUrl = "http://localhost:54872/api/ValidateTaskName";
        private const string EditTaskUrl = "http://localhost:54872/api/ValidateAndEditTaskName";
        public TaskManagerService(IUnitOfWork unitOfWork, IMapper mapper, IRequestService requestService)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _requestService = requestService ?? throw new ArgumentNullException(nameof(requestService));
        }
        public EditTaskViewModel GetTaskById(int? id)
        {
            var task = _unitOfWork.Assignment.GetTaskById(id);

            var taskViewModel = _mapper.Map<Assignment, EditTaskViewModel>(task);

            taskViewModel.Status = (AssignmentStatus)task.TaskStatus;

            return taskViewModel;
        }
        public void AddTaskToDatabase(TaskViewModel task)
        {
            task.LastModified = DateTime.Now;

            bool result = CheckTaskNameUniqueness(task);

            if (!result)
            {
                task.NrOfTimesTaskCreated = _unitOfWork.Assignment.GetTotalTasksCountByTaskName(task.TaskName) + 1;

                Assignment assignment = _mapper.Map<TaskViewModel, Assignment>(task);

                _unitOfWork.Assignment.AddTaskToDatabase(assignment);
                _unitOfWork.Complete();
            }
        }

        private bool CheckTaskNameUniqueness(TaskViewModel task)
        {
            var validateTaskDto = Mapper
                .Map<TaskViewModel, ValidateTaskRequestDto>(task);

            WebResponseDto result = _requestService
                .SendPostRequest(ValidateTaskNameUrl, JsonConvert.SerializeObject(validateTaskDto));

            if (result.HttpStatusCode == HttpStatusCode.OK)
            {
                ValidationResultDto validationResult = JsonConvert
                    .DeserializeObject<ValidationResultDto>(result.Response);

                return validationResult.IsFailure;
            }

            return true;
        }

        public void EditTask(EditTaskViewModel task)
        {
            var taskToEdit = _unitOfWork.Assignment.GetTaskById(task.Id);

            if (taskToEdit != null && IsSameDateTime(task.LastModified, taskToEdit.LastModified))
            {
                var editRequestDto = BuildEditTaskRequest(task, taskToEdit);

                WebResponseDto result = _requestService
                    .SendPostRequest(EditTaskUrl, JsonConvert.SerializeObject(editRequestDto));

                if (result.HttpStatusCode == HttpStatusCode.OK)
                {
                    taskToEdit.TaskName = task.TaskName;
                    taskToEdit.TaskStatus = (int)task.Status;
                    taskToEdit.LastModified = DateTime.Now;
                    taskToEdit.NrOfTimesTaskUpdated += 1;

                    _unitOfWork.Complete();
                }
            }
        }
        public List<TaskViewModel> GetOpenTasksFromDatabase()
        {
            var tasks = _unitOfWork.Assignment
                .GetOpenTasksFromDatabase();

            return BuildTaskViewModelList(tasks);
        }

        private List<TaskViewModel> BuildTaskViewModelList(List<Assignment> assignment)
        {
            var tasksList =
                new List<TaskViewModel>();

            foreach (var task in assignment)
            {
                tasksList.Add(
                    new TaskViewModel
                    {
                        Id = task.Id,
                        TaskName = task.TaskName,
                        TaskStatus = ((AssignmentStatus)task.TaskStatus).ToString(),
                        NrOfTimesTaskUpdated = task.NrOfTimesTaskUpdated,
                        NrOfTimesTaskCreated = task.NrOfTimesTaskCreated,
                        LastModified = task.LastModified
                    });
            }

            return tasksList;
        }

        private EditTaskRequestDto BuildEditTaskRequest(
            EditTaskViewModel newTask, Assignment oldTask)
        {
            return new EditTaskRequestDto
            {
                NewTaskName = newTask.TaskName,
                OldTaskName = oldTask.TaskName,
                TaskStatus = newTask.Status
            };
        }
        private bool IsSameDateTime(DateTime d1, DateTime d2)
        {
            DateTime date1 = new DateTime(d1.Year, d1.Month, d1.Day, d1.Hour, d1.Minute, d1.Second);
            DateTime date2 = new DateTime(d2.Year, d2.Month, d2.Day, d2.Hour, d2.Minute, d2.Second);

            return date1 == date2;
        }
    }
}
