using Vexed.Models;

namespace Vexed.Services.Abstractions
{
    public interface IContactInfoService
    {
        void CreateContactInfo(ContactInfo contactInfo);
        void UpdateContactInfo(ContactInfo contactInfo);
        void DeleteContactInfo(ContactInfo contactInfo);
        ContactInfo GetContactInfoById(int id);
        Task<List<ContactInfo>> GetAllContacts();
        List<ContactInfo> GetContactInfos(Guid userId);
        List<string> GetContactTypes();
    }
}
