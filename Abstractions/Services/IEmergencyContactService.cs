using Vexed.Models;

namespace Vexed.Services.Abstractions
{
    public interface IEmergencyContactService
    {
        /// <summary>
        /// Creates a new Emergency Contact.
        /// </summary>
        void CreateEmergencyContact(EmergencyContact emergencyContact);

        /// <summary>
        /// Updates the details of an Emergency Contact.
        /// </summary>
        void UpdateEmergencyContact(EmergencyContact emergencyContact);

        /// <summary>
        /// Removes Emergency Contact from the database.
        /// </summary>
        void DeleteEmergencyContact(EmergencyContact emergencyContact);

        /// <summary>
        /// Returns the first Emergency Contact with the given <c>id</c>
        /// </summary>
        EmergencyContact GetEmergencyContactById(int id);

        /// <summary>
        /// Returns all the Emergency Contacts from the database.
        /// </summary>
        List<EmergencyContact> GetAllEmergencyContacts();

        /// <summary>
        /// Returns all the Emergency Contacts for a given user.
        /// </summary>
        List<EmergencyContact> GetEmergencyContacts(Guid userId);

        /// <summary>
        /// Method <c>GetContactTypes</c> returns a list of all Relationships for Emergency Contacts.
        /// </summary>
        public List<string> GetRelationshipTypes();

        /// <summary>
        /// Method <c>GetContactTypes</c> returns a list of all Relationships for Emergency Contacts. Moves selected
        /// item to the first position.
        /// </summary>
        public List<string> GetRelationshipTypes(string selectedRelationship);

    }
}
