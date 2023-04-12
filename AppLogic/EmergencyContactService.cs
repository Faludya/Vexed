using Microsoft.EntityFrameworkCore;
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

        public async Task CreateEmergencyContact(EmergencyContact emergencyContact)
        {
            await _repositoryWrapper.EmergencyContactRepository.Create(emergencyContact);
            await _repositoryWrapper.Save();
        }

        public async Task DeleteEmergencyContact(EmergencyContact emergencyContact)
        {
            await _repositoryWrapper.EmergencyContactRepository.Delete(emergencyContact);
            await _repositoryWrapper.Save();
        }

        public async Task<List<EmergencyContact>> GetAllEmergencyContacts()
        {
            var queryable = await _repositoryWrapper.EmergencyContactRepository.FindAll();
            return await queryable.ToListAsync();
        }

        public async Task<EmergencyContact> GetEmergencyContactById(int id)
        {
            return await _repositoryWrapper.EmergencyContactRepository.GetEmergencyContactById(id);
        }

        public async Task<List<EmergencyContact>> GetEmergencyContacts(Guid userId)
        {
            return await _repositoryWrapper.EmergencyContactRepository.GetEmergencyContacts(userId);
        }

        public async Task UpdateEmergencyContact(EmergencyContact emergencyContact)
        {
            await _repositoryWrapper.EmergencyContactRepository.Update(emergencyContact);
            await _repositoryWrapper.Save();
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
