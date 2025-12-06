using MIS_Lab4.Models;
using System;
using System.Collections.Generic;

class Program
{
    // Danh sách tài khoản mẫu
    static Dictionary<string, UserAccount> accounts = new Dictionary<string, UserAccount>()
    {
        { "admin", new UserAccount { Username = "admin", Password = "admin123", Role = "Admin" } },
        { "user1", new UserAccount { Username = "user1", Password = "user123", Role = "User" } }
    };

    static void Main(string[] args)
    {
        Console.WriteLine("=== MIS Lab 4 – Basic Login ===");
        
        Console.Write("Enter username: ");
        string username = Console.ReadLine();

        Console.Write("Enter password: ");
        string password = Console.ReadLine();

        // Kiểm tra username có tồn tại không
        if (!accounts.ContainsKey(username))
        {
            Console.WriteLine("Invalid credentials!");
        }
        // Kiểm tra password
        else if (BasicLogin(username, password))
        {
            Console.WriteLine("Login successful!");
        }
        else
        {
            Console.WriteLine("Invalid password");
        }
    }

    // Hàm login cơ bản
    static bool BasicLogin(string username, string password)
    {
        // Kiểm tra tài khoản có tồn tại không
        if (!accounts.ContainsKey(username))
        {
            return false;
        }

        // Lấy thông tin user
        var account = accounts[username];

        // Kiểm tra password
        return account.Password == password;
    }
}

