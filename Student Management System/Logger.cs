namespace Student_Management_System.utils
{
    public class Logger
    {
            public static void LogError(Exception ex)
            {
                string logFile = "error_log.txt";
                string message = $"[{DateTime.Now}] {ex.Message}\n{ex.StackTrace}\n";

                File.AppendAllText(logFile, message);
        }
    }
}
