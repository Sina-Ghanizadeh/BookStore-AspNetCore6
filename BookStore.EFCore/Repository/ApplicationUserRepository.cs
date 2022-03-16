using BookStore.Domain.UserAgg;

namespace BookStore.EFCore.Repository
{
    public class ApplicationUserRepository : RepositoryBase<ApplicationUser>, IApplicationUserRepository
    {
        private readonly DatabaseContext _context;

        public ApplicationUserRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        
    }
}
