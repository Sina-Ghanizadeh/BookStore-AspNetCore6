using BookStore.Application.Contracts.CoverType;

namespace BookStore.Domain.CoverTypeAgg
{
    public interface ICoverTypeRepository : IRepositoryBase<CoverType>
    {
        void Update(CoverType coverType);

        EditCoverType GetDetails(int id);

        List<CoverTypeViewModel> GetCoverTypes();
    }
}
