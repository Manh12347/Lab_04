using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIS_Lab4.Models;
using MIS_Lab4.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MIS_Lab4.Tests
{
    [TestClass]
    public class PerformanceTests
    {
        private Dictionary<string, UserAccount> GetSampleAccounts()
        {
            return new Dictionary<string, UserAccount>()
            {
                { "admin", new UserAccount { Username="admin", Password="admin123", Role="Admin" } },
                { "user1", new UserAccount { Username="user1", Password="user123", Role="User" } }
            };
        }

        [TestMethod]
        public void Login_Performance_1000_Times()
        {
            var accounts = GetSampleAccounts();
            var auth = new AuthService(accounts, new FakeLogService());

            var sw = new Stopwatch();
            sw.Start();

            for (int i = 0; i < 1000; i++)
                auth.Authenticate("admin", "admin123");

            sw.Stop();

            Console.WriteLine($"Login 1000 times: {sw.ElapsedMilliseconds} ms");
            Console.WriteLine($"Average per login: {(double)sw.ElapsedMilliseconds / 1000:F3} ms");

            Assert.IsTrue(sw.ElapsedMilliseconds < 500); // Yêu cầu hiệu năng: < 500ms
        }

        [TestMethod]
        public void AddUser_Performance_1000_Times()
        {
            var accounts = GetSampleAccounts();
            var initialCount = accounts.Count; // 2 users ban đầu
            var ums = new UserManagementService(accounts, new FakeLogService());

            var sw = new Stopwatch();
            sw.Start();

            // Bắt đầu từ user2 để tránh trùng với user1 có sẵn
            for (int i = 2; i < 1002; i++)
                ums.AddUser($"user{i}", "123", "User");

            sw.Stop();

            Console.WriteLine($"Add 1000 users: {sw.ElapsedMilliseconds} ms");
            Console.WriteLine($"Average per add: {(double)sw.ElapsedMilliseconds / 1000:F3} ms");

            Assert.IsTrue(sw.ElapsedMilliseconds < 600); // Yêu cầu hiệu năng: < 600ms
            Assert.AreEqual(initialCount + 1000, accounts.Count); // 2 ban đầu + 1000 mới
        }

        [TestMethod]
        public void AddEmployee_Performance_1000_Times()
        {
            var es = new EmployeeService();
            var sw = new Stopwatch();

            sw.Start();

            for (int i = 0; i < 1000; i++)
                es.AddEmployee(new Employee { Id = $"E{i:D4}", Name = "Test", Email = "a@mail.com" });

            sw.Stop();

            Console.WriteLine($"Add 1000 employees: {sw.ElapsedMilliseconds} ms");
            Console.WriteLine($"Average per add: {(double)sw.ElapsedMilliseconds / 1000:F3} ms");

            Assert.IsTrue(sw.ElapsedMilliseconds < 600); // Yêu cầu hiệu năng: < 600ms
            Assert.AreEqual(1000, es.GetAllEmployees().Count);
        }

        [TestMethod]
        public void CRUD_Combined_Performance()
        {
            var es = new EmployeeService();
            var sw = new Stopwatch();

            sw.Start();

            // ADD
            for (int i = 0; i < 500; i++)
                es.AddEmployee(new Employee { Id = $"E{i:D4}", Name = "Test", Email = "mail@test.com" });

            // UPDATE
            for (int i = 0; i < 500; i++)
                es.UpdateEmployee(new Employee { Id = $"E{i:D4}", Name = "Updated", Email = "updated@mail.com" });

            // GET
            for (int i = 0; i < 500; i++)
                es.GetEmployeeById($"E{i:D4}");

            // DELETE
            for (int i = 0; i < 500; i++)
                es.DeleteEmployee($"E{i:D4}");

            sw.Stop();

            Console.WriteLine($"CRUD 500 employees (Add+Update+Get+Delete): {sw.ElapsedMilliseconds} ms");
            Console.WriteLine($"Average per operation: {(double)sw.ElapsedMilliseconds / 2000:F3} ms");

            Assert.IsTrue(sw.ElapsedMilliseconds < 800); // Yêu cầu hiệu năng: < 800ms
            Assert.AreEqual(0, es.GetAllEmployees().Count); // Tất cả đã bị xóa
        }
    }
}

