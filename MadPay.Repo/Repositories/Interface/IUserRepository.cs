using MadPay.Data.Models;
using MadPay.Repo.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MadPay.Repo.Repositories.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> UserExists(string username);
    }
}
