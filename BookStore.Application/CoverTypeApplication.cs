using BookStore.Application.Contracts.CoverType;
using BookStore.Domain.CoverTypeAgg;
using UtilityProject.Application;

namespace BookStore.Application
{
    public class CoverTypeApplication : ICoverTypeApplication
    {
        private readonly ICoverTypeRepository _coverTypeRepository;

        public CoverTypeApplication(ICoverTypeRepository coverTypeRepository)
        {
            _coverTypeRepository = coverTypeRepository;
        }

        public string Create(CreateCoverType command)
        {
           if(_coverTypeRepository.IsExists(x=>x.Name ==  command.Name))
                return ApplicationMessages.DuplicatedRecord;

           CoverType cover = new()
           {
               Name = command.Name
           };
            _coverTypeRepository.Add(cover);
            _coverTypeRepository.Save();

            return ApplicationMessages.Success;
        }

        public string Delete(int id)
        {
            var cover = _coverTypeRepository.GetFirstOrDefault(x=>x.Id == id);
            if(cover == null)
                return ApplicationMessages.RecordNotFound;

            _coverTypeRepository.Delete(cover);
            _coverTypeRepository.Save();
            return ApplicationMessages.Success;
        }

        public string Edit(EditCoverType command)
        {
            var cover = _coverTypeRepository.GetFirstOrDefault(x=>x.Id == command.Id);
            if(cover == null)
                return ApplicationMessages.RecordNotFound;
            if(_coverTypeRepository.IsExists(x=>x.Name == command.Name))
                return ApplicationMessages.DuplicatedRecord;

            cover.Name = command.Name;
            _coverTypeRepository.Update(cover);
            _coverTypeRepository.Save();

            return ApplicationMessages.Success;
        }

        public List<CoverTypeViewModel> GetCoverTypes()
        {
            return _coverTypeRepository.GetCoverTypes();
        }

        public EditCoverType GetDetails(int id)
        {
            return _coverTypeRepository.GetDetails(id);
        }
    }
}
