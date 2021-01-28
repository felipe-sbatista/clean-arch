using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using clean.application.Models;

namespace clean.application.Interfaces
{
    public interface IUserAppService
    {
        Task<List<UserDto>> ListAllAsync();
        Task<int> RegisterAsync(UserDto dto);
    }
}
