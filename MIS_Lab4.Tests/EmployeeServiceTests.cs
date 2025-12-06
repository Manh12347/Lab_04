using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIS_Lab4.Models;
using MIS_Lab4.Services;

namespace MIS_Lab4.Tests
{
    [TestClass]
    public class EmployeeServiceTests
    {
        private EmployeeService service;

        [TestInitialize]
        public void Setup()
        {
            service = new EmployeeService();
        }

        [TestMethod]
        public void AddEmployee_Success()
        {
            var emp = new Employee { Id = "E01", Name = "John", Email = "john@mail.com" };
            var result = service.AddEmployee(emp);

            Assert.IsTrue(result);
            var added = service.GetEmployeeById("E01");
            Assert.IsNotNull(added);
            Assert.AreEqual("John", added.Name);
        }

        [TestMethod]
        public void AddEmployee_Fail_DuplicateID()
        {
            var emp1 = new Employee { Id = "E01", Name = "John", Email = "john@mail.com" };
            service.AddEmployee(emp1);

            var emp2 = new Employee { Id = "E01", Name = "Jane", Email = "jane@mail.com" };
            var result = service.AddEmployee(emp2);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DeleteEmployee_Success()
        {
            var emp = new Employee { Id = "E01", Name = "John", Email = "john@mail.com" };
            service.AddEmployee(emp);

            var result = service.DeleteEmployee("E01");

            Assert.IsTrue(result);
            var deleted = service.GetEmployeeById("E01");
            Assert.IsNull(deleted);
        }

        [TestMethod]
        public void DeleteEmployee_Fail_NotFound()
        {
            var result = service.DeleteEmployee("E99");

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void UpdateEmployee_Success()
        {
            service.AddEmployee(new Employee { Id = "E01", Name = "A", Email = "a@mail.com" });

            var updated = new Employee { Id = "E01", Name = "B", Email = "b@mail.com" };
            var result = service.UpdateEmployee(updated);

            Assert.IsTrue(result);
            var emp = service.GetEmployeeById("E01");
            Assert.IsNotNull(emp);
            Assert.AreEqual("B", emp.Name);
            Assert.AreEqual("b@mail.com", emp.Email);
        }

        [TestMethod]
        public void UpdateEmployee_Fail_NotFound()
        {
            var emp = new Employee { Id = "E99", Name = "Test", Email = "test@mail.com" };
            var result = service.UpdateEmployee(emp);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetEmployeeById_Success()
        {
            var emp = new Employee { Id = "E01", Name = "John", Email = "john@mail.com" };
            service.AddEmployee(emp);

            var result = service.GetEmployeeById("E01");

            Assert.IsNotNull(result);
            Assert.AreEqual("E01", result.Id);
            Assert.AreEqual("John", result.Name);
            Assert.AreEqual("john@mail.com", result.Email);
        }

        [TestMethod]
        public void GetEmployeeById_Fail_NotFound()
        {
            var result = service.GetEmployeeById("E99");

            Assert.IsNull(result);
        }
    }
}

