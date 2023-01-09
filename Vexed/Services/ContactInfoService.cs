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

        public void UpdateContactInfo(ContactInfo contactInfo)
        {
            _repositoryWrapper.ContactInfoRepository.Update(contactInfo);
            _repositoryWrapper.Save();
        }
    }
}