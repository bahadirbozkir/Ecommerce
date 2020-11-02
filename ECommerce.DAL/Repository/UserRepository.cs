using ECommerce.DAL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECommerce.DAL.Repository
{
    public class UserRepository : IUserRepository
    {

        private List<User> _users = new List<User>();

        public UserRepository()
        {
            _users.Add(new User { Username = "admin", Password = "password", Userrole = (int)UserRoleEnum.Admin });
            _users.Add(new User { Username = "user", Password = "password", Userrole = (int)UserRoleEnum.User });
            _users.Add(new User { Username = "moderator", Password = "password", Userrole = (int)UserRoleEnum.Moderator});
        }

        public bool IsUserAuthenticated(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            string decodedAuthenticationToken =
                Encoding.UTF8.GetString(Convert.FromBase64String(token));
            string[] usernamePasswordArray = decodedAuthenticationToken.Split(':');
            string username = usernamePasswordArray[0];
            string password = usernamePasswordArray[1];

            var user = _users.FirstOrDefault(x => x.Username == username && x.Password == password);

            if (user != null && user.Userrole == (int)UserRoleEnum.Admin)
            {
                return true;
            }
            else
            {
                return false;
            }            
        }
    }
}
