using TaskTracker_CLI.Enums;
using TaskTracker_CLI.Services;

class Program
{
    static void Main(string[] args)
    {
        var taskService = new TaskService();

        if (args.Length == 0)
        {
            PrintHelp();
            return;
        }

        string command = args[0].ToLower();

        switch (command)
        {
            
            case "add":
                if (args.Length < 2)
                {
                    Console.WriteLine("Description required.");
                    return;
                }

                var created = taskService.AddTask(args[1]);
                Console.WriteLine($"Task created -> ID: {created.Id} | Description: {created.Description}");
                break;

            
            case "update":
                if (args.Length < 3)
                {
                    Console.WriteLine("Usage: update <id> \"new description\"");
                    return;
                }

                int updateId = int.Parse(args[1]);
                var updated = taskService.UpdateTask(updateId, args[2]);

                if (updated == null)
                    Console.WriteLine($"Task with ID {updateId} not found.");
                else
                    Console.WriteLine($"Task updated -> ID: {updated.Id} | Description: {updated.Description}");
                break;

            
            case "delete":
                if (args.Length < 2)
                {
                    Console.WriteLine("Usage: delete <id>");
                    return;
                }

                int deleteId = int.Parse(args[1]);

                if (taskService.DeleteTask(deleteId))
                    Console.WriteLine($"Task deleted -> ID: {deleteId}");
                else
                    Console.WriteLine($"Task with ID {deleteId} not found.");
                break;

            
            case "mark-in-progress":
                if (args.Length < 2)
                {
                    Console.WriteLine("Usage: mark-in-progress <id>");
                    return;
                }

                int progressId = int.Parse(args[1]);
                var progressTask = taskService.MarkInProgress(progressId);

                if (progressTask == null)
                    Console.WriteLine($"Task with ID {progressId} not found.");
                else
                    Console.WriteLine($"Task marked as In-Progress -> ID: {progressTask.Id}");
                break;

            
            case "mark-done":
                if (args.Length < 2)
                {
                    Console.WriteLine("Usage: mark-done <id>");
                    return;
                }

                int doneId = int.Parse(args[1]);
                var doneTask = taskService.MarkDone(doneId);

                if (doneTask == null)
                    Console.WriteLine($"Task with ID {doneId} not found.");
                else
                    Console.WriteLine($"Task marked as Done -> ID: {doneTask.Id}");
                break;

            
            case "list":

                // list all
                if (args.Length == 1)
                {
                    var allTasks = taskService.GetAllTasks();
                    Console.WriteLine("\nAll Tasks:\n");

                    foreach (var t in allTasks)
                        Console.WriteLine($"[{t.Id}] {t.Description}  [{t.TaskStatus}]");

                    return;
                }

                
                string filter = args[1].ToLower();

                Status status = filter switch
                {
                    "todo" => Status.Todo,
                    "done" => Status.Done,
                    "in-progress" => Status.InProgress,
                    _ => throw new Exception("Invalid filter. Valid values: todo | done | in-progress")
                };

                var filtered = taskService.GetTasksByStatus(status);

                Console.WriteLine($"\nTasks ({status}):\n");

                foreach (var t in filtered)
                    Console.WriteLine($"[{t.Id}] {t.Description}");

                break;

  
            default:
                Console.WriteLine("Unknown command.");
                PrintHelp();
                break;
        }
    }


    static void PrintHelp()
    {
        Console.WriteLine("\nAvailable Commands:");
        Console.WriteLine(" add \"description\"");
        Console.WriteLine(" update <id> \"new description\"");
        Console.WriteLine(" delete <id>");
        Console.WriteLine(" mark-in-progress <id>");
        Console.WriteLine(" mark-done <id>");
        Console.WriteLine(" list");
        Console.WriteLine(" list todo | done | in-progress\n");
    }
}
