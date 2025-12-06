using MIS_Lab4.Models;
using System;
using System.Collections.Generic;

namespace MIS_Lab4.Services
{
    public class UserManagementService
    {
        private Dictionary<string, UserAccount> accounts;
        private LogService logger;

        public UserManagementService(Dictionary<string, UserAccount> accounts, LogService logger)
        {
            this.accounts = accounts;
            this.logger = logger;
        }

        public void ViewAllUsers()
        {
            Console.WriteLine("\n=== USER LIST ===");
            Console.WriteLine("Username\tRole\tLocked");

            foreach (var acc in accounts.Values)
            {
                Console.WriteLine($"{acc.Username}\t{acc.Role}\t{acc.IsLocked}");
            }
        }

        public List<UserAccount> GetAllUsers()
        {
            return new List<UserAccount>(accounts.Values);
        }

        public bool AddUser(string username, string password, string role)
        {
            if (accounts.ContainsKey(username))
            {
                return false; // Trùng username
            }

            accounts[username] = new UserAccount
            {
                Username = username,
                Password = password,
                Role = role,
                IsLocked = false
            };

            logger.WriteLog($"Admin added new user: {username}");
            return true;
        }

        public bool UnlockUser(string username)
        {
            if (!accounts.ContainsKey(username))
            {
                return false; // Không tồn tại
            }

            accounts[username].IsLocked = false;
            logger.WriteLog($"Admin unlocked account: {username}");
            return true;
        }

        public void AddUser()
        {
            Console.Write("\nEnter new username: ");
            string username = Console.ReadLine();

            if (accounts.ContainsKey(username))
            {
                Console.WriteLine("User already exists!");
                return;
            }

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            Console.Write("Enter role (Admin/User): ");
            string role = Console.ReadLine();

            accounts[username] = new UserAccount
            {
                Username = username,
                Password = password,
                Role = role,
                IsLocked = false
            };

            logger.WriteLog($"Admin added new user: {username}");
            Console.WriteLine("User added successfully.");
        }

        public void DeleteUser(string currentAdmin)
        {
            Console.Write("\nEnter username to delete: ");
            string username = Console.ReadLine();

            if (!accounts.ContainsKey(username))
            {
                Console.WriteLine("User does not exist!");
                return;
            }

            if (username == currentAdmin)
            {
                Console.WriteLine("You cannot delete yourself!");
                return;
            }

            accounts.Remove(username);
            logger.WriteLog($"Admin deleted user: {username}");
            Console.WriteLine("User deleted successfully.");
        }

        public void UnlockUser()
        {
            Console.Write("\nEnter username to unlock: ");
            string username = Console.ReadLine();

            if (!accounts.ContainsKey(username))
            {
                Console.WriteLine("User does not exist!");
                return;
            }

            accounts[username].IsLocked = false;
            logger.WriteLog($"Admin unlocked account: {username}");
            Console.WriteLine("Account unlocked.");
        }
    }
}

