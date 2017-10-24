﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAuth.Web.Services
{
    public interface IUserService
    {
        Task<bool> AddUser(string username, string password);
        Task<bool> VerifyCredentials(string username, string password, out User user);
    }

    public class User
    {
        public User(string username)
        {
            Username = username;
        }

        public string Username { get; private set; }
    }
}
