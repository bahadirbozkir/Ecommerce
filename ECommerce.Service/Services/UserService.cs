using ECommerce.DAL.Repository;
using ECommerce.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool DoLogin(string token)
        {            
            var value = _userRepository.IsUserAuthenticated(token);

            return value;
        }
    }
}
