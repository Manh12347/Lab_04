using MIS_Lab4.Models;
using System;
using System.Collections.Generic;

namespace MIS_Lab4.Services
{
    public class AuthService
    {
        private Dictionary<string, UserAccount> accounts;
        private Dictionary<string, int> loginAttempts;
        private LogService logger;

        public AuthService(Dictionary<string, UserAccount> accounts, LogService logger)
        {
            this.accounts = accounts;
            this.logger = logger;
            loginAttempts = new Dictionary<string, int>();

            foreach (var acc in accounts.Keys)
                loginAttempts[acc] = 0;
        }

        public UserAccount Authenticate(string username, string password)
        {
            if (!accounts.ContainsKey(username))
            {
                logger.WriteLog($"Failed login attempt for unknown user {username}");
                return null;
            }

            var user = accounts[username];

            if (user.IsLocked)
            {
                logger.WriteLog($"Login attempt on locked account {username}");
                Console.WriteLine("Account locked!");
                return null;
            }

            if (user.Password == password)
            {
                loginAttempts[user.Username] = 0;
                logger.WriteLog($"User {username} logged in successfully.");
                return user;
            }
            else
            {
                loginAttempts[user.Username]++;
                logger.WriteLog($"Failed login attempt for {username}");

                if (loginAttempts[user.Username] >= 3)
                {
                    user.IsLocked = true;
                    logger.WriteLog($"User {username} account locked after 3 failed attempts.");
                    Console.WriteLine("Account locked!");
                }

                return null;
            }
        }
    }
}

