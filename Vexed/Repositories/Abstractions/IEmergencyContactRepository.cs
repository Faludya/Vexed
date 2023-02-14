using Vexed.Models;

namespace Vexed.Repositories.Abstractions
{
    public interface IEmergencyContactRepository : IRepositoryBase<EmergencyContact>
    {
        /// <summary>
        /// Returns the first Emergency Contact with the given <c>id</c>
        /// </summary>
        EmergencyContact GetEmergencyContactById(int id);

        /// <summary>
        /// Returns all the Emergency Contacts for a given user.
        /// </summary>
        List<EmergencyContact> GetEmergencyContacts(Guid userId);
    }
}