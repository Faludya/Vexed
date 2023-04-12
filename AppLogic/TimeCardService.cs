using Microsoft.EntityFrameworkCore;
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

        public async Task CreateTimeCard(TimeCard timeCard)
        {
            timeCard.SuperiorId = await _repositoryWrapper.UserRepository.GetUserSuperior(timeCard.UserId);
            timeCard.Status = StatusManager.SetStatus(timeCard.Status);

            await _repositoryWrapper.TimeCardRepository.Create(timeCard);
            await _repositoryWrapper.Save();
        }

        public async Task DeleteTimeCard(TimeCard timeCard)
        {
            await _repositoryWrapper.TimeCardRepository.Delete(timeCard);
            await _repositoryWrapper.Save();
        }

        public async Task<List<TimeCard>> GetAllTimeCards()
        {
            var queryable = await _repositoryWrapper.TimeCardRepository.FindAll();
            return await queryable.ToListAsync();
        }

        public async Task<TimeCard> GetTimeCardById(int id)
        {
            return await _repositoryWrapper.TimeCardRepository.GetTimeCardById(id);
        }

        public async Task<List<TimeCard>> GetTimeCards(Guid userId)
        {
            return await _repositoryWrapper.TimeCardRepository.GetTimeCards(userId);
        }

        public async Task UpdateTimeCard(TimeCard timeCard)
        {
            await _repositoryWrapper.TimeCardRepository.Update(timeCard);
            await _repositoryWrapper.Save();
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

        public async Task<List<TimeCard>> GetTimeCardsForSuperior(Guid superiorId)
        {
            return await _repositoryWrapper.TimeCardRepository.GetTimeCardsSuperior(superiorId);
        }

        public async Task<TimeCard> GetLastTimeCard(Guid userId)
        {            
            var lastTimeCard = await _repositoryWrapper.TimeCardRepository.GetLastTimeCard(userId);
            lastTimeCard.Status = StatusManager.ResetStatus();
            lastTimeCard.StartDate = FirstDayOfWeek(DateTime.Now);
            lastTimeCard.EndDate = lastTimeCard.StartDate.AddDays(4);

            return lastTimeCard;
        }

        /// <summary>
        /// First day of the week is dependent on the selected culture.
        /// </summary>
        private DateTime FirstDayOfWeek(DateTime dt)
        {
            var culture = Thread.CurrentThread.CurrentCulture;
            var diff = dt.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
            if (diff < 0)
                diff += 7;
            return dt.AddDays(-diff).Date;
        }
    }
}
