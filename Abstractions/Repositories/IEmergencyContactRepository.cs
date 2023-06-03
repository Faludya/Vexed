using DataModels;

namespace Vexed.Repositories.Abstractions
{
    public interface IEmergencyContactRepository : IRepositoryBase<EmergencyContact>
    {
        /// <summary>
        /// Returns the first Emergency Contact with the given <c>id</c>
        /// </summary>
        Task<EmergencyContact> GetEmergencyContactById(int id);

        /// <summary>
        /// Returns all the Emergency Contacts for a given user.
        /// </summary>
        Task<List<EmergencyContact>> GetEmergencyContacts(Guid userId);
    }
}