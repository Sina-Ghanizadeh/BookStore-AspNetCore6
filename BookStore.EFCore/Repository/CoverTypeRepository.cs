using BookStore.Application.Contracts.CoverType;
using BookStore.Domain.CoverTypeAgg;

namespace BookStore.EFCore.Repository
{
    public class CoverTypeRepository : RepositoryBase<CoverType>, ICoverTypeRepository
    {
        private readonly DatabaseContext _context;

        public CoverTypeRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public List<CoverTypeViewModel> GetCoverTypes()
        {
            return _context.CoverTypes.Select(c => new CoverTypeViewModel
            {
                Id = c.Id,
                Name = c.Name,

            }).ToList();
        }

        public EditCoverType GetDetails(int id)
        {
            return _context.CoverTypes.Select(c => new EditCoverType
            {
                Id = c.Id,
                Name = c.Name,

            }).SingleOrDefault(x=>x.Id == id);
        }

        public void Update(CoverType coverType)
        {
            _context.Update(coverType);
        }
    }
}
