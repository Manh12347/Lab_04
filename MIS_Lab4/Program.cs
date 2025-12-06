using MIS_Lab4.Models;
using MIS_Lab4.Services;
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        var accounts = new Dictionary<string, UserAccount>()
        {
            { "admin", new UserAccount { Username="admin", Password="admin123", Role="Admin" } },
            { "user1", new UserAccount { Username="user1", Password="user123", Role="User" } }
        };

        LogService logger = new LogService();
        AuthService auth = new AuthService(accounts, logger);
        MenuService menu = new MenuService();
        UserManagementService userService = new UserManagementService(accounts, logger);

        while (true)
        {
            Console.Write("\nUsername: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            var user = auth.Authenticate(username, password);

            if (user != null)
            {
                Console.WriteLine("Login successful!");

                if (user.Role == "Admin")
                    menu.ShowAdminMenu(userService, user.Username);
                else
                    menu.ShowUserMenu();
            }
            else
            {
                // Xử lý thông báo lỗi
                if (username != null && accounts.ContainsKey(username))
                {
                    var acc = accounts[username];
                    if (acc.IsLocked)
                    {
                        // Đã hiển thị "Account locked!" trong AuthService
                    }
                    else
                    {
                        Console.WriteLine("Invalid password");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid credentials!");
                }
            }
        }
    }
}

