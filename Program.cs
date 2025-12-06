using MIS_Lab4.Models;
using MIS_Lab4.Services;
using System;
using System.Collections.Generic;

class Program
{
    static Dictionary<string, UserAccount> accounts = new Dictionary<string, UserAccount>()
    {
        { "admin", new UserAccount { Username="admin", Password="admin123", Role="Admin" } },
        { "user1", new UserAccount { Username="user1", Password="user123", Role="User" } }
    };

    static void Main(string[] args)
    {
        AuthService auth = new AuthService(accounts);

        while (true)
        {
            Console.WriteLine("\n=== MIS LOGIN ADVANCED ===");

            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            var user = auth.Authenticate(username, password);

            if (user != null) // login success
            {
                Console.WriteLine("Login successful!");

                if (user.Role == "Admin")
                {
                    Console.WriteLine("Welcome Admin! You have full access.");
                    // Task 008: Menu Admin
                }
                else
                {
                    Console.WriteLine("Welcome User! You have limited access.");
                    // Task 008: Menu User
                }
            }
            else
            {
                // Xử lý thông báo lỗi
                if (username != null && accounts.ContainsKey(username))
                {
                    var account = accounts[username];
                    if (account.IsLocked)
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

