using Vexed.Models;

namespace Vexed.Services.Abstractions
{
    public interface IEmergencyContactService
    {
        void CreateEmergencyContact(EmergencyContact emergencyContact);
        void UpdateEmergencyContact(EmergencyContact emergencyContact);
        void DeleteEmergencyContact(EmergencyContact emergencyContact);
        List<EmergencyContact> GetAllEmergencyContacts();
    }
}
