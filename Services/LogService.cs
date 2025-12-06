using System;
using System.IO;

namespace MIS_Lab4.Services
{
    public class LogService
    {
        private readonly string logPath = "Logs/system_log.txt";

        public LogService()
        {
            // Tạo thư mục Logs nếu chưa tồn tại
            if (!Directory.Exists("Logs"))
                Directory.CreateDirectory("Logs");

            // Tạo file nếu chưa có
            if (!File.Exists(logPath))
                File.WriteAllText(logPath, "");
        }

        public void WriteLog(string message)
        {
            string line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
            File.AppendAllLines(logPath, new[] { line });
        }
    }
}

