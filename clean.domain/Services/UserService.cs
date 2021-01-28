using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using clean.domain.Interfaces.Repositories;
using clean.domain.Interfaces.Services;
using clean.domain.Models;

namespace clean.domain.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> FindByIdAsync(int id)
        {

            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<User>> GetAllAsync()
        {

            return await _repository.ListAllAsync();
        }

        public async Task<User> RegisterAsync(User user)
        {
            return await _repository.AddAsync(user);
        }
    }
}
