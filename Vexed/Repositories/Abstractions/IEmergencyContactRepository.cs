using Vexed.Models;

namespace Vexed.Repositories.Abstractions
{
    public interface IEmergencyContactRepository : IRepositoryBase<EmergencyContact>
    {
        EmergencyContact GetEmergencyContactById(int id);
        List<EmergencyContact> GetEmergencyContacts(Guid userId);
    }
}