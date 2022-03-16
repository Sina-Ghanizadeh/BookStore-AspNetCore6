using BookStore.Application.Contracts.Company;
using BookStore.Domain.CompanyAgg;
using UtilityProject.Application;

namespace BookStore.Application
{
    public class CompanyApplication : ICompanyApplication
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyApplication(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public string Create(CreateCompany command)
        {
            if (_companyRepository.IsExists(x => x.Name == command.Name))
                return ApplicationMessages.DuplicatedRecord;


            Company company = new()
            {
                Name = command.Name,
                City = command.City,
                PhoneNumber = command.PhoneNumber,
                PostalCode = command.PostalCode,
                State = command.State,
                StreetAddress = command.StreetAddress,
            };

            _companyRepository.Add(company);
            _companyRepository.Save();
            return ApplicationMessages.Success;
        }

        public string Delete(int id)
        {
            var company = _companyRepository.GetFirstOrDefault(x => x.Id == id);
            if (company == null)
                return ApplicationMessages.RecordNotFound;

            _companyRepository.Delete(company);
            _companyRepository.Save();
            return ApplicationMessages.Success;
        }

        public string Edit(EditCompany command)
        {
            var company = _companyRepository.GetFirstOrDefault(c => c.Id == command.Id);
            if (company == null)
                return ApplicationMessages.RecordNotFound;
            

            company.Name = command.Name;
            company.City = command.City;
            company.PhoneNumber = command.PhoneNumber;
            company.PostalCode = command.PostalCode;
            company.State = command.State;
            company.StreetAddress = command.StreetAddress;

            _companyRepository.Update(company);
            _companyRepository.Save();

            return ApplicationMessages.Success;
        }

        public List<CompanyViewModel> GetAllCompanies()
        {
            return _companyRepository.GetAllCompanies();
        }

        public EditCompany GetDetails(int id)
        {
            return _companyRepository.GetDetails(id);
        }
    }
}
