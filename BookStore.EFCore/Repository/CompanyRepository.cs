using BookStore.Application.Contracts.Company;
using BookStore.Domain.CompanyAgg;

namespace BookStore.EFCore.Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        private readonly DatabaseContext _context;

        public CompanyRepository(DatabaseContext context) : base(context)
        {
            _context = context;
        }

        public List<CompanyViewModel> GetAllCompanies()
        {
            return _context.Companies.Select(c => new CompanyViewModel { 
                
                Id = c.Id,
                City = c.City,
                Name = c.Name,
                PhoneNumber = c.PhoneNumber,
                PostalCode = c.PostalCode,
                State = c.State,
                StreetAddress = c.StreetAddress
                
                }).ToList();
        }

        public EditCompany GetDetails(int id)
        {
           return _context.Companies.Select(c=>new EditCompany
           {
               Id = c.Id,
               City = c.City,
               Name = c.Name,
               PhoneNumber = c.PhoneNumber,
               PostalCode = c.PostalCode,
               State = c.State,
               StreetAddress = c.StreetAddress
               
           }).FirstOrDefault(c=>c.Id == id);
        }

        public void Update(Company company)
        {
            _context.Update(company);
        }
    }
}
