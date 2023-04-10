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

        public void CreateContactInfo(ContactInfo contactInfo)
        {
            _repositoryWrapper.ContactInfoRepository.Create(contactInfo);
            _repositoryWrapper.Save();
        }

        public void DeleteContactInfo(ContactInfo contactInfo)
        {
            _repositoryWrapper.ContactInfoRepository.Delete(contactInfo);
            _repositoryWrapper.Save();
        }

        public async Task<List<ContactInfo>> GetAllContacts()
        {
            return await _repositoryWrapper.ContactInfoRepository.FindAll().ToListAsync();
        }

        public ContactInfo GetContactInfoById(int id)
        {
            return _repositoryWrapper.ContactInfoRepository.GetContactInfoById(id);
        }

        public List<ContactInfo> GetContactInfos(Guid userId)
        {
            return _repositoryWrapper.ContactInfoRepository.GetContactInfos(userId);
        }

        public void UpdateContactInfo(ContactInfo contactInfo)
        {
            _repositoryWrapper.ContactInfoRepository.Update(contactInfo);
            _repositoryWrapper.Save();
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