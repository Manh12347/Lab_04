using MIS_Lab4.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace MIS_Lab4.Services
{
    public class AuthService
    {
        private Dictionary<string, UserAccount> accounts;
        private Dictionary<string, int> loginAttempts;
        private string logPath = "Logs/system_log.txt";

        public AuthService(Dictionary<string, UserAccount> accounts)
        {
            this.accounts = accounts;
            loginAttempts = new Dictionary<string, int>();

            // Khởi tạo loginAttempts = 0 cho mỗi user
            foreach (var acc in accounts.Keys)
                loginAttempts[acc] = 0;
        }

        private void Log(string message)
        {
            string line = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
            File.AppendAllLines(logPath, new[] { line });
            Console.WriteLine(line); // optional
        }

        public UserAccount Authenticate(string username, string password)
        {
            // Không tồn tại user
            if (!accounts.ContainsKey(username))
            {
                Log($"Failed login attempt for {username}");
                return null;
            }

            var user = accounts[username];

            // Nếu user đã bị khóa
            if (user.IsLocked)
            {
                Console.WriteLine("Account locked!");
                Log($"Login attempt for locked account {username}");
                return null;
            }

            // Kiểm tra password
            if (user.Password == password)
            {
                loginAttempts[user.Username] = 0; // reset đếm
                Log($"User {username} logged in successfully.");
                return user;
            }
            else
            {
                loginAttempts[user.Username]++;

                Log($"Failed login attempt for {username}");

                if (loginAttempts[user.Username] >= 3)
                {
                    user.IsLocked = true;
                    Console.WriteLine("Account locked!");
                    Log($"User {username} account locked after 3 failed attempts.");
                }

                return null;
            }
        }
    }
}

