using Microsoft.EntityFrameworkCore;
using Shared;
using Vexed.Models;
using Vexed.Repositories.Abstractions;
using Vexed.Services.Abstractions;

namespace Vexed.Services
{
    public class TimeCardService : ITimeCardService
    {
        private IRepositoryWrapper _repositoryWrapper;
        private Logger _logger;

        public TimeCardService(IRepositoryWrapper repositoryWrapper, Logger logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        public async Task CreateTimeCard(TimeCard timeCard)
        {
            try
            {
                timeCard.SuperiorId = await _repositoryWrapper.UserRepository.GetUserSuperior(timeCard.UserId);
                timeCard.Status = StatusManager.SetStatus(timeCard.Status);

                await _repositoryWrapper.TimeCardRepository.Create(timeCard);
                await _repositoryWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task DeleteTimeCard(TimeCard timeCard)
        {
            try
            {
                await _repositoryWrapper.TimeCardRepository.Delete(timeCard);
                await _repositoryWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<TimeCard>> GetAllTimeCards()
        {
            try
            {
                var queryable = await _repositoryWrapper.TimeCardRepository.FindAll();
                return await queryable.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<TimeCard> GetTimeCardById(int id)
        {
            try
            {
                return await _repositoryWrapper.TimeCardRepository.GetTimeCardById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<TimeCard>> GetTimeCards(Guid userId)
        {
            try
            {
                return await _repositoryWrapper.TimeCardRepository.GetTimeCards(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task UpdateTimeCard(TimeCard timeCard)
        {
            try
            {
                await _repositoryWrapper.TimeCardRepository.Update(timeCard);
                await _repositoryWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public List<string> GetLocationTypes()
        {
            try
            {
                var contactTypes = new List<string>()
                {
                    "Work from Office", "Work from Home", "Short term relocated", "Relocated"
                };

                return contactTypes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public List<string> GetLocationTypes(string selectedLocation)
        {
            try
            {
                var locationTypes = new List<string>()
                {
                    "Work from Office", "Work from Home", "Short term relocated", "Relocated"
                };
                int posSelected = locationTypes.IndexOf(selectedLocation);
                (locationTypes[0], locationTypes[posSelected]) = (locationTypes[posSelected], locationTypes[0]);

                return locationTypes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<TimeCard>> GetTimeCardsForSuperior(Guid superiorId)
        {
            try
            {
                return await _repositoryWrapper.TimeCardRepository.GetTimeCardsSuperior(superiorId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<TimeCard> GetLastTimeCard(Guid userId)
        {
            try
            {
                var lastTimeCard = await _repositoryWrapper.TimeCardRepository.GetLastTimeCard(userId);
                lastTimeCard.Status = StatusManager.ResetStatus();
                lastTimeCard.StartDate = FirstDayOfWeek(DateTime.Now);
                lastTimeCard.EndDate = lastTimeCard.StartDate.AddDays(4);

                return lastTimeCard;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        /// <summary>
        /// First day of the week is dependent on the selected culture.
        /// </summary>
        private DateTime FirstDayOfWeek(DateTime dt)
        {
            try
            {
                var culture = Thread.CurrentThread.CurrentCulture;
                var diff = dt.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
                if (diff < 0)
                    diff += 7;
                return dt.AddDays(-diff).Date;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
