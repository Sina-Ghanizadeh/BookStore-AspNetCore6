using BookStore.Application.Contracts.Company;

namespace BookStore.Domain.CompanyAgg
{
    public interface ICompanyRepository : IRepositoryBase<Company>
    {
        void Update(Company company);
        EditCompany GetDetails(int id);

        List<CompanyViewModel> GetAllCompanies();
    }
}
