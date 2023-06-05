using Microsoft.EntityFrameworkCore;
using Shared;
using DataModels;
using Vexed.Repositories.Abstractions;
using Vexed.Services.Abstractions;

namespace Vexed.Services
{
    public class ContactInfoService : IContactInfoService
    {
        private IRepositoryWrapper _repositoryWrapper;
        private Logger _logger;

        public ContactInfoService(IRepositoryWrapper repositoryWrapper, Logger logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        public async Task CreateContactInfo(ContactInfo contactInfo)
        {
            try
            {
                await _repositoryWrapper.ContactInfoRepository.Create(contactInfo);
                await _repositoryWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }

        }

        public async Task DeleteContactInfo(ContactInfo contactInfo)
        {
            try
            {
                await _repositoryWrapper.ContactInfoRepository.Delete(contactInfo);
                await _repositoryWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<ContactInfo>> GetAllContacts()
        {
            try
            {
                var queryable = await _repositoryWrapper.ContactInfoRepository.FindAll();
                return await queryable.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<ContactInfo> GetContactInfoById(int id)
        {
            try
            {
                return await _repositoryWrapper.ContactInfoRepository.GetContactInfoById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<ContactInfo>> GetContactInfos(Guid userId)
        {
            try
            {
                return await _repositoryWrapper.ContactInfoRepository.GetContactInfos(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task UpdateContactInfo(ContactInfo contactInfo)
        {
            try
            {
                await _repositoryWrapper.ContactInfoRepository.Update(contactInfo);
                await _repositoryWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public List<string> GetContactTypes()
        {
            try
            {
                var contactTypes = new List<string>()
                {
                    "Work Phone", "Personal Phone", "Work Email", "Personal Email", "Fax"
                };

                return contactTypes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public List<string> GetContactTypes(string selectedType)
        {
            try
            {
                var contactTypes = new List<string>()
                {
                    "Work Phone", "Personal Phone", "Work Email", "Personal Email", "Fax"
                };
                int posSelected = contactTypes.IndexOf(selectedType);
                (contactTypes[0], contactTypes[posSelected]) = (contactTypes[posSelected], contactTypes[0]);

                return contactTypes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}