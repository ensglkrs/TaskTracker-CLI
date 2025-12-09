using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker_CLI.Enums;
using TaskTracker_CLI.Models;


namespace TaskTracker_CLI.Interfaces
{
    public interface ITaskService
    {
        AppTask AddTask(string Description);
        AppTask? UpdateTask(int id, string newDescription);
        bool DeleteTask(int id);
        AppTask? MarkInProgress (int id);
        List<AppTask> GetAllTasks();
        List<AppTask> GetTasksByStatus(Status status);

    }
}
