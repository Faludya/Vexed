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
            timeCard.SuperiorId = _repositoryWrapper.UserRepository.GetUserSuperior(timeCard.UserId);
            
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

        public List<TimeCard> GetTimeCards(Guid userId)
        {
            return _repositoryWrapper.TimeCardRepository.GetTimeCards(userId);
        }

        public void UpdateTimeCard(TimeCard timeCard)
        {
            _repositoryWrapper.TimeCardRepository.Update(timeCard);
            _repositoryWrapper.Save();
        }

        public List<string> GetLocationTypes()
        {
            var contactTypes = new List<string>()
            {
                "Work from Office", "Work from Home", "Short term relocated", "Relocated"
            };

            return contactTypes;
        }

        public List<string> GetLocationTypes(string selectedLocation)
        {
            var locationTypes = new List<string>()
            {
                "Work from Office", "Work from Home", "Short term relocated", "Relocated"
            };
            int posSelected = locationTypes.IndexOf(selectedLocation);
            (locationTypes[0], locationTypes[posSelected]) = (locationTypes[posSelected], locationTypes[0]);

            return locationTypes;
        }

        public List<TimeCard> GetTimeCardsForSuperior(Guid superiorId)
        {
            return _repositoryWrapper.TimeCardRepository.GetTimeCardsSuperior(superiorId);
        }
    }
}
