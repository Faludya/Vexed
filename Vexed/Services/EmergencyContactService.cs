using Vexed.Models;
using Vexed.Repositories.Abstractions;
using Vexed.Services.Abstractions;

namespace Vexed.Services
{
    public class EmergencyContactService : IEmergencyContactService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public EmergencyContactService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public void CreateEmergencyContact(EmergencyContact emergencyContact)
        {
            _repositoryWrapper.EmergencyContactRepository.Create(emergencyContact);
            _repositoryWrapper.Save();
        }

        public void DeleteEmergencyContact(EmergencyContact emergencyContact)
        {
            _repositoryWrapper.EmergencyContactRepository.Delete(emergencyContact);
            _repositoryWrapper.Save();
        }

        public List<EmergencyContact> GetAllEmergencyContacts()
        {
            return _repositoryWrapper.EmergencyContactRepository.FindAll().ToList();
        }

        public EmergencyContact GetEmergencyContactById(int id)
        {
            return _repositoryWrapper.EmergencyContactRepository.GetEmergencyContactById(id);
        }

        public List<EmergencyContact> GetEmergencyContacts(Guid userId)
        {
            return _repositoryWrapper.EmergencyContactRepository.GetEmergencyContacts(userId);
        }

        public void UpdateEmergencyContact(EmergencyContact emergencyContact)
        {
            _repositoryWrapper.EmergencyContactRepository.Update(emergencyContact);
            _repositoryWrapper.Save();
        }

        public List<string> GetRelationshipTypes()
        {
            var relationshipTypes = new List<string>()
            {
                "Parent", "Brother", "Sister", "Partner", "Child", "Relative", "Friend"
            };

            return relationshipTypes;
        }

        public List<string> GetRelationshipTypes(string selectedRelationship)
        {
            var relationshipTypes = new List<string>()
            {
                "Parent", "Brother", "Sister", "Partner", "Child", "Relative", "Friend"
            };
            int posSelected = relationshipTypes.IndexOf(selectedRelationship);
            (relationshipTypes[0], relationshipTypes[posSelected]) = (relationshipTypes[posSelected], relationshipTypes[0]);

            return relationshipTypes;
        }
    }
}
