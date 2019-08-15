
using MadPay.Data.DatabaseContext;
using MadPay.Data.Models;
using MadPay.Repo.Infrastructure;
using MadPay.Repo.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MadPay724.Repo.Repositories.Repo
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly DbContext _db;
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
            _db = _db ?? (MadpayDbContext)_db;
        }

        public async Task<bool> UserExists(string username)
        {
            if (await GetAsync(p => p.UserName == username) != null)
                return true;

            return false;
        }
    }
}