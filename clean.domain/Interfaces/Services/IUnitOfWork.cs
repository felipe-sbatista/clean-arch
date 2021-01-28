using System;
using System.Threading.Tasks;

namespace clean.domain.Interfaces.Services
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();
    }
}
