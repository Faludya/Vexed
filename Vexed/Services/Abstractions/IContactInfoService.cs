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
        /// <summary>
        /// Method <c>GetContactTypes</c> returns a list of all Types of Contacts.
        /// </summary>
        List<string> GetContactTypes();

        /// <summary>
        /// Method <c>GetContactTypes</c> returns a list of all Types of Contacts and moves the selected type to the first position.
        /// </summary>
        List<string> GetContactTypes(string selectedType);
    }
}
