using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using clean.domain.Models;

namespace clean.domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<User> FindByIdAsync(int id);
        Task<List<User>> GetAllAsync();
        Task<User> RegisterAsync(User user);
    }
}
