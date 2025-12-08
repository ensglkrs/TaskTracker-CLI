using TaskTracker.Enums;
using TaskTracker.Models;
using System.Collections.Generic;

namespace TaskTracker.Interfaces
{
    public interface ITaskService
    {
        AppTask AddTask(string Description);
        AppTask? UpdateTask(int id, string newDescription);
        bool DeleteTask(int id);
        AppTask? MarkInProgress(int id);
        AppTask? MarkDone(int id);
        List<AppTask> GetAllTasks();
        List<AppTask> GetTasksByStatus(Status status);

    }
}
