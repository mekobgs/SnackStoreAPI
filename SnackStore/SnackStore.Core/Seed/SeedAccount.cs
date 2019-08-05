using SnackStore.Core.Helpers;
using SnackStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnackStore.Core.Seed
{
    public class SeedAccount
    {
        public static List<Account> SeedData()
        {
            return new List<Account>
            {
                new Account()
                {
                    User = "admin",
                    Password = "admin".ToSha256(),
                    Role = Role.Admin
                },
                new Account()
                {
                    User = "guest",
                    Password = "guest".ToSha256(),
                    Role = Role.Guest
                }
            };
        }
    }
}
