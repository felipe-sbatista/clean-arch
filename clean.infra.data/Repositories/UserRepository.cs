using System;
using clean.domain.Interfaces.Repositories;
using clean.domain.Models;
using clean.infra.data.Repositories.Base;

namespace clean.infra.data.Repositories
{
    public class UserRepository:BaseRepository<User>, IUserRepository
    {
        public UserRepository(CleanContext context):base(context)
        {
        }
    }
}
