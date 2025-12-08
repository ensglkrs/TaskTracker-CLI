using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TaskTracker.Enums;
using TaskTracker.Interfaces;
using TaskTracker.Models;
using TaskTracker.Utilities;


namespace TaskTracker.Services
{
    public class TaskService : ITaskService
    {
        private readonly string _filePath;
        private List<AppTask> _tasks;

        public TaskService()
        {
            _filePath = Utility.GetFilePath();
            Utility.EnsureFileExists();
            _tasks = LoadTasksFromFile();
        }

        // ─────────── ITaskService Metotları (Doldurulacak) ───────────
        public AppTask AddTask(string description)
        {
            
            int newId = GetNextId();

            
            var newTask = new AppTask
            {
                Id = newId,
                Description = description,
                TaskStatus = Status.Todo,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            
            _tasks.Add(newTask);
            
            SaveTasksToFile();

            return newTask;
        }


        public AppTask? UpdateTask(int id, string newDescription)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
                return null;

            task.Description = newDescription;
            task.UpdatedAt = DateTime.Now;

            SaveTasksToFile();
      
            return task;
        }

        public bool DeleteTask(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);

            if (task == null) 
                return false;

            _tasks.Remove(task);

            SaveTasksToFile();

            return true;
        }

        public AppTask? MarkInProgress(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
                return null;

            task.TaskStatus = Status.InProgress;
            task.UpdatedAt = DateTime.Now;

            SaveTasksToFile();

            return task;
        }

        public AppTask? MarkDone(int id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
                return null;

            task.TaskStatus = Status.Done;
            task.UpdatedAt = DateTime.Now;

            SaveTasksToFile();

            return task;
        }

        public List<AppTask> GetAllTasks()
        {
            return _tasks;
        }

        public List<AppTask> GetTasksByStatus(Status status)
        {
            return _tasks.Where(t => t.TaskStatus == status).ToList();
        }

        // ─────────────── JSON İşlemleri ───────────────

        private List<AppTask> LoadTasksFromFile()
        {
            string fileContent = File.ReadAllText(_filePath);
            var tasks = JsonSerializer.Deserialize<List<AppTask>>(fileContent);

            return tasks ?? new List<AppTask>();
        }

        private void SaveTasksToFile()
        {
            string json = JsonSerializer.Serialize(_tasks);
            File.WriteAllText(_filePath, json);
        }

        private int GetNextId()
        {
            if (_tasks.Count == 0)
                return 1;

            return _tasks.Max(t => t.Id) + 1;
        }

    }
}
