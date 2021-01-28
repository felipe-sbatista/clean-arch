using System.Threading.Tasks;
using clean.domain.Interfaces.Services;

namespace clean.infra.data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CleanContext _context;

        public UnitOfWork(CleanContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
