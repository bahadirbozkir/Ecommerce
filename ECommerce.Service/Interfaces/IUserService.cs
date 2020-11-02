using ECommerce.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Service.Interfaces
{
    public interface IUserService
    {
        bool DoLogin(string token);
    }
}
