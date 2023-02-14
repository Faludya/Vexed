using Vexed.Models;

namespace Vexed.Services.Abstractions
{
    public interface IEmergencyContactService
    {
        void CreateEmergencyContact(EmergencyContact emergencyContact);
        void UpdateEmergencyContact(EmergencyContact emergencyContact);
        void DeleteEmergencyContact(EmergencyContact emergencyContact);
        EmergencyContact GetEmergencyContactById(int id);
        List<EmergencyContact> GetAllEmergencyContacts();
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
