using Microsoft.EntityFrameworkCore;
using Shared;
using DataModels;
using Vexed.Repositories.Abstractions;
using Vexed.Services.Abstractions;

namespace Vexed.Services
{
    public class EmergencyContactService : IEmergencyContactService
    {
        private IRepositoryWrapper _repositoryWrapper;
        private Logger _logger;

        public EmergencyContactService(IRepositoryWrapper repositoryWrapper, Logger logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        public async Task CreateEmergencyContact(EmergencyContact emergencyContact)
        {
            try
            {
                await _repositoryWrapper.EmergencyContactRepository.Create(emergencyContact);
                await _repositoryWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task DeleteEmergencyContact(EmergencyContact emergencyContact)
        {
            try
            {
                await _repositoryWrapper.EmergencyContactRepository.Delete(emergencyContact);
                await _repositoryWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<EmergencyContact>> GetAllEmergencyContacts()
        {
            try
            {
                var queryable = await _repositoryWrapper.EmergencyContactRepository.FindAll();
                return await queryable.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<EmergencyContact> GetEmergencyContactById(int id)
        {
            try
            {
                return await _repositoryWrapper.EmergencyContactRepository.GetEmergencyContactById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<EmergencyContact>> GetEmergencyContacts(Guid userId)
        {
            try
            {
                return await _repositoryWrapper.EmergencyContactRepository.GetEmergencyContacts(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task UpdateEmergencyContact(EmergencyContact emergencyContact)
        {
            try
            {
                await _repositoryWrapper.EmergencyContactRepository.Update(emergencyContact);
                await _repositoryWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public List<string> GetRelationshipTypes()
        {
            try
            {
                var relationshipTypes = new List<string>()
                {
                    "Parent", "Brother", "Sister", "Partner", "Child", "Relative", "Friend"
                };

                return relationshipTypes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public List<string> GetRelationshipTypes(string selectedRelationship)
        {
            try
            {
                var relationshipTypes = new List<string>()
                {
                    "Parent", "Brother", "Sister", "Partner", "Child", "Relative", "Friend"
                };
                int posSelected = relationshipTypes.IndexOf(selectedRelationship);
                (relationshipTypes[0], relationshipTypes[posSelected]) = (relationshipTypes[posSelected], relationshipTypes[0]);

                return relationshipTypes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
