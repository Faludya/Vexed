using Microsoft.EntityFrameworkCore;
using Vexed.Models;
using Vexed.Repositories.Abstractions;
using Vexed.Services.Abstractions;

namespace Vexed.Services
{
    public class ContactInfoService : IContactInfoService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public ContactInfoService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task CreateContactInfo(ContactInfo contactInfo)
        {
            await _repositoryWrapper.ContactInfoRepository.Create(contactInfo);
            await _repositoryWrapper.Save();
        }

        public async Task DeleteContactInfo(ContactInfo contactInfo)
        {
            await _repositoryWrapper.ContactInfoRepository.Delete(contactInfo);
            await _repositoryWrapper.Save();
        }

        public async Task<List<ContactInfo>> GetAllContacts()
        {
            var queryable = await _repositoryWrapper.ContactInfoRepository.FindAll();
            return await queryable.ToListAsync();
        }

        public async Task<ContactInfo> GetContactInfoById(int id)
        {
            return await _repositoryWrapper.ContactInfoRepository.GetContactInfoById(id);
        }

        public async Task<List<ContactInfo>> GetContactInfos(Guid userId)
        {
            return await _repositoryWrapper.ContactInfoRepository.GetContactInfos(userId);
        }

        public async Task UpdateContactInfo(ContactInfo contactInfo)
        {
            await _repositoryWrapper.ContactInfoRepository.Update(contactInfo);
            await _repositoryWrapper.Save();
        }

        public List<string> GetContactTypes()
        {
            var contactTypes = new List<string>()
            {
                "Work Phone", "Personal Phone", "Work Email", "Personal Email", "Fax"
            };

            return contactTypes;
        }

        public List<string> GetContactTypes(string selectedType)
        {
            var contactTypes = new List<string>()
            {
                "Work Phone", "Personal Phone", "Work Email", "Personal Email", "Fax"
            };
            int posSelected = contactTypes.IndexOf(selectedType);
            (contactTypes[0], contactTypes[posSelected]) = (contactTypes[posSelected], contactTypes[0]);

            return contactTypes;
        }
    }
}