using Vexed.Models;
using Vexed.Repositories.Abstractions;
using Vexed.Services.Abstractions;

namespace Vexed.Services
{
    public class TimeCardService : ITimeCardService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public TimeCardService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public void CreateTimeCard(TimeCard timeCard)
        {
            _repositoryWrapper.TimeCardRepository.Create(timeCard);
            _repositoryWrapper.Save();
        }

        public void DeleteTimeCard(TimeCard timeCard)
        {
            _repositoryWrapper.TimeCardRepository.Delete(timeCard); 
            _repositoryWrapper.Save();
        }

        public List<TimeCard> GetAllTimeCards()
        {
            return _repositoryWrapper.TimeCardRepository.FindAll().ToList();
        }

        public TimeCard GetTimeCardById(int id)
        {
            return _repositoryWrapper.TimeCardRepository.GetTimeCardById(id);
        }

        public void UpdateTimeCard(TimeCard timeCard)
        {
            _repositoryWrapper.TimeCardRepository.Update(timeCard);
            _repositoryWrapper.Save();
        }
    }
}
