using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using clean.application.Interfaces;
using clean.application.Models;
using clean.domain.Interfaces.Services;
using clean.domain.Models;

namespace clean.application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserService _service;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserAppService(IUserService service, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _service = service;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> ListAllAsync()
        {
            var users = await _service.GetAllAsync();
            var usersDto = _mapper.Map<List<UserDto>>(users);
            return usersDto;
        }

        public async Task<int> RegisterAsync(UserDto dto)
        {
            try
            {
                // use cqrs or automapper
                var user = new User { Name = dto.Name, Age = dto.Age};
                var newUser = await _service.RegisterAsync(user);
                if(await _unitOfWork.CommitAsync())
                {
                    return newUser.Id;
                }
                // implement error handler
                return 0;
            }
            catch (Exception ex)
            {
                // implement error handler
                return 0;
            }
        }
    }
}
