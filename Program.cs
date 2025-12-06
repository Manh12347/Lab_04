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

        while (true)
        {
            Console.WriteLine("\n=== LOGIN ADVANCED ===");
            Console.Write("Username: ");
            string user = Console.ReadLine();
            Console.Write("Password: ");
            string pass = Console.ReadLine();

            var account = auth.Authenticate(user, pass);

            if (account != null)
            {
                Console.WriteLine("Login successful!");
            }
            else
            {
                // Xử lý thông báo lỗi
                if (user != null && accounts.ContainsKey(user))
                {
                    var acc = accounts[user];
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

