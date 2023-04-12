using Vexed.Models;

namespace Vexed.Services.Abstractions
{
    public interface IEmergencyContactService
    {
        /// <summary>
        /// Creates a new Emergency Contact.
        /// </summary>
        Task CreateEmergencyContact(EmergencyContact emergencyContact);

        /// <summary>
        /// Updates the details of an Emergency Contact.
        /// </summary>
        Task UpdateEmergencyContact(EmergencyContact emergencyContact);

        /// <summary>
        /// Removes Emergency Contact from the database.
        /// </summary>
        Task DeleteEmergencyContact(EmergencyContact emergencyContact);

        /// <summary>
        /// Returns the first Emergency Contact with the given <c>id</c>
        /// </summary>
        Task<EmergencyContact> GetEmergencyContactById(int id);

        /// <summary>
        /// Returns all the Emergency Contacts from the database.
        /// </summary>
        Task<List<EmergencyContact>> GetAllEmergencyContacts();

        /// <summary>
        /// Returns all the Emergency Contacts for a given user.
        /// </summary>
        Task<List<EmergencyContact>> GetEmergencyContacts(Guid userId);

        /// <summary>
        /// Method <c>GetContactTypes</c> returns a list of all Relationships for Emergency Contacts.
        /// </summary>
        List<string> GetRelationshipTypes();

        /// <summary>
        /// Method <c>GetContactTypes</c> returns a list of all Relationships for Emergency Contacts. Moves selected
        /// item to the first position.
        /// </summary>
        List<string> GetRelationshipTypes(string selectedRelationship);

    }
}
