using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIS_Lab4.Models;
using MIS_Lab4.Services;
using System.Collections.Generic;
using System.Linq;

namespace MIS_Lab4.Tests
{
    [TestClass]
    public class IntegrationTests
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
        public void Admin_Login_And_ViewUsers()
        {
            var accounts = GetSampleAccounts();
            var auth = new AuthService(accounts, new FakeLogService());

            var admin = auth.Authenticate("admin", "admin123");

            Assert.IsNotNull(admin);
            Assert.AreEqual("Admin", admin.Role);

            var ums = new UserManagementService(accounts, new FakeLogService());
            var list = ums.GetAllUsers();

            Assert.IsTrue(list.Count >= 1);
            Assert.IsTrue(list.Any(u => u.Username == "admin"));
        }

        [TestMethod]
        public void Admin_AddUser_Then_LoginWithNewUser()
        {
            var accounts = GetSampleAccounts();
            var ums = new UserManagementService(accounts, new FakeLogService());

            bool added = ums.AddUser("newuser", "123456", "User");
            Assert.IsTrue(added);

            var auth = new AuthService(accounts, new FakeLogService());
            var result = auth.Authenticate("newuser", "123456");

            Assert.IsNotNull(result);
            Assert.AreEqual("newuser", result.Username);
            Assert.AreEqual("User", result.Role);
        }

        [TestMethod]
        public void Admin_LockUser_Then_UserCannotLogin()
        {
            var accounts = GetSampleAccounts();
            var ums = new UserManagementService(accounts, new FakeLogService());

            // Khóa user1
            accounts["user1"].IsLocked = true;

            var auth = new AuthService(accounts, new FakeLogService());
            var result = auth.Authenticate("user1", "user123");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Admin_AddEmployee_Then_ViewEmployeeList()
        {
            var empService = new EmployeeService();
            var emp = new Employee { Id = "E01", Name = "John", Email = "john@mail.com" };

            bool added = empService.AddEmployee(emp);
            Assert.IsTrue(added);

            var list = empService.GetAllEmployees();
            Assert.IsTrue(list.Count >= 1);
            Assert.IsTrue(list.Any(e => e.Id == "E01"));
        }

        [TestMethod]
        public void Admin_Update_Employee_Then_Get()
        {
            var empService = new EmployeeService();
            empService.AddEmployee(new Employee { Id = "E01", Name = "John", Email = "john@mail.com" });

            var updated = new Employee { Id = "E01", Name = "Jane", Email = "jane@mail.com" };
            bool result = empService.UpdateEmployee(updated);
            Assert.IsTrue(result);

            var emp = empService.GetEmployeeById("E01");
            Assert.IsNotNull(emp);
            Assert.AreEqual("Jane", emp.Name);
            Assert.AreEqual("jane@mail.com", emp.Email);
        }

        [TestMethod]
        public void Admin_Delete_Employee_Then_CannotFind()
        {
            var empService = new EmployeeService();
            empService.AddEmployee(new Employee { Id = "E01", Name = "John", Email = "john@mail.com" });

            bool deleted = empService.DeleteEmployee("E01");
            Assert.IsTrue(deleted);

            var emp = empService.GetEmployeeById("E01");
            Assert.IsNull(emp);
        }

        [TestMethod]
        public void NormalUser_CannotAccess_AdminMenu()
        {
            var accounts = GetSampleAccounts();
            var auth = new AuthService(accounts, new FakeLogService());

            var user = auth.Authenticate("user1", "user123");

            Assert.IsNotNull(user);
            Assert.AreEqual("User", user.Role);
            Assert.AreNotEqual("Admin", user.Role);
        }
    }
}

