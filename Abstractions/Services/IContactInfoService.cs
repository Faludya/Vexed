using Vexed.Models;

namespace Vexed.Services.Abstractions
{
    public interface IContactInfoService
    {
        /// <summary>
        /// Creates a new Contact Info.
        /// </summary>
        void CreateContactInfo(ContactInfo contactInfo);

        /// <summary>
        /// Updates the details of a Contact Info.
        /// </summary>
        void UpdateContactInfo(ContactInfo contactInfo);

        /// <summary>
        /// Removes the Contact Info from the database.
        /// </summary>
        void DeleteContactInfo(ContactInfo contactInfo);

        /// <summary>
        /// Returns the first Contact Info with the given <c>id</c>
        /// </summary>
        ContactInfo GetContactInfoById(int id);

        /// <summary>
        /// Returns all the Contacts from the database.
        /// </summary>
        Task<List<ContactInfo>> GetAllContacts();

        /// <summary>
        /// Returns all the Contact Infos for a given user. 
        /// </summary>
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
