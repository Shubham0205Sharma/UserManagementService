using UserManagement.Domain.Interfaces;
using UserManagement.Infrastructure.Data;

namespace UserManagement.Infrastructure.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IUserRepository Users { get; private set; }
        public IRoleRepository Roles { get; private set; }

        public UnitOfWork(AppDbContext context, IUserRepository users, IRoleRepository roles)
        {
            _context = context;
            Users = users;
            Roles = roles;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
