using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAuth.Web.Services
{
    public class DummyUserService : IUserService
    {
        private IDictionary<string, Tuple<string, User>> _users =
            new Dictionary<string, Tuple<string, User>>();

        public DummyUserService(IDictionary<string, string> users)
        {
            foreach (var user in users)
            {
                _users.Add(user.Key.ToLower(),
                    Tuple.Create(BCrypt.Net.BCrypt.HashPassword(user.Value, 10), new User(user.Key)));
            }
        }

        public Task<bool> AddUser(string username, string password)
        {
            if (_users.ContainsKey(username.ToLower()))
            {
                return Task.FromResult(false);
            }
            _users.Add(username.ToLower(),
                Tuple.Create(BCrypt.Net.BCrypt.HashPassword(password, 10), new User(username)));
            return Task.FromResult(true);
        }

        public Task<bool> VerifyCredentials(string username, string password, out User user)
        {
            var key = username.ToLower();
            if (_users.ContainsKey(key))
            {
                var hash = _users[key].Item1;
                if (BCrypt.Net.BCrypt.Verify(password, hash))
                {
                    user = _users[key].Item2;
                    return Task.FromResult(true);
                }
            }
            user = null;
            return Task.FromResult(false);
        }
    }
}
