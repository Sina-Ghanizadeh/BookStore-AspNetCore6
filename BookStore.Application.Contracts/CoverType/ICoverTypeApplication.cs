using BookStore.Application.Contracts.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.Contracts.CoverType
{
    public interface ICoverTypeApplication
    {
        string Create(CreateCoverType command);
        string Edit(EditCoverType command);

        string Delete(int id);

        EditCoverType GetDetails(int id);

        List<CoverTypeViewModel> GetCoverTypes();

    }
}
