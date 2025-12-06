using System;

namespace MIS_Lab4.Services
{
    public class MenuService
    {
        public void ShowAdminMenu()
        {
            while (true)
            {
                Console.WriteLine("\n=== ADMIN MENU ===");
                Console.WriteLine("1. View all users");
                Console.WriteLine("2. Add new user");
                Console.WriteLine("3. Delete user");
                Console.WriteLine("4. Unlock account");
                Console.WriteLine("5. View system logs");
                Console.WriteLine("0. Logout");

                Console.Write("Choose: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Feature: View all users (placeholder)");
                        break;
                    case "2":
                        Console.WriteLine("Feature: Add user (placeholder)");
                        break;
                    case "3":
                        Console.WriteLine("Feature: Delete user (placeholder)");
                        break;
                    case "4":
                        Console.WriteLine("Feature: Unlock account (placeholder)");
                        break;
                    case "5":
                        Console.WriteLine("Feature: Show logs (placeholder)");
                        break;
                    case "0":
                        return; // logout
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            }
        }

        public void ShowUserMenu()
        {
            while (true)
            {
                Console.WriteLine("\n=== USER MENU ===");
                Console.WriteLine("1. View personal info");
                Console.WriteLine("2. Change password");
                Console.WriteLine("0. Logout");

                Console.Write("Choose: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Feature: View personal info (placeholder)");
                        break;
                    case "2":
                        Console.WriteLine("Feature: Change password (placeholder)");
                        break;
                    case "0":
                        return; 
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            }
        }
    }
}

