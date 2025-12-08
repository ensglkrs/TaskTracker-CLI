using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Utilities
{
    public class Utility
    {
        public const string FileName = "tasks.json";
        public static string GetFilePath() 
        {
            return Path.Combine(Directory.GetCurrentDirectory(), FileName);
        }
        public static void EnsureFileExists() 
        {
            var path = GetFilePath();
            
            if (!File.Exists(path)) 
            {
                File.WriteAllText(path, "[]");
            }
        }
    }
}
