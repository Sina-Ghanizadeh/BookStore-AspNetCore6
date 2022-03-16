using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Contracts.Company
{
    public interface ICompanyApplication
    {

        string Create(CreateCompany command);
        string Edit(EditCompany command);
        string Delete(int id);
        EditCompany GetDetails(int id);

        List<CompanyViewModel> GetAllCompanies();

    }
}
