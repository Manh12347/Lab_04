using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIS_Lab4.Models;
using MIS_Lab4.Services;
using System.Collections.Generic;

namespace MIS_Lab4.Tests
{
    [TestClass]
    public class AuthServiceTests
    {
        private AuthService auth;
        private Dictionary<string, UserAccount> accounts;

        [TestInitialize]
        public void Setup()
        {
            accounts = new Dictionary<string, UserAccount>()
            {
                { "admin", new UserAccount { Username="admin", Password="admin123", Role="Admin" } },
                { "user1",  new UserAccount { Username="user1", Password="123", Role="User" } }
            };

            LogService logger = new FakeLogService();
            auth = new AuthService(accounts, logger);
        }

        [TestMethod]
        public void Login_Success()
        {
            var result = auth.Authenticate("admin", "admin123");
            Assert.IsNotNull(result);
            Assert.AreEqual("admin", result.Username);
        }

        [TestMethod]
        public void Login_Fail_WrongPassword()
        {
            var result = auth.Authenticate("user1", "wrongpass");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Login_Fail_UnknownUser()
        {
            var result = auth.Authenticate("unknown", "123");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Account_Locked_After_3_Attempts()
        {
            auth.Authenticate("user1", "wrong1");
            auth.Authenticate("user1", "wrong2");
            auth.Authenticate("user1", "wrong3");

            Assert.IsTrue(accounts["user1"].IsLocked);
        }

        [TestMethod]
        public void Login_Fail_When_Already_Locked()
        {
            // Khóa trước
            accounts["user1"].IsLocked = true;

            var result = auth.Authenticate("user1", "123");

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Reset_LoginAttempt_On_Success()
        {
            // nhập sai 1 lần
            auth.Authenticate("user1", "wrong");

            // nhập đúng -> reset
            var result = auth.Authenticate("user1", "123");

            Assert.IsNotNull(result);
            Assert.IsFalse(accounts["user1"].IsLocked);
        }
    }
}
