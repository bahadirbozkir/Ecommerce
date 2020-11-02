using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.DAL.Repository
{
    public interface IUserRepository
    {
        bool IsUserAuthenticated(string token);
    }
}
