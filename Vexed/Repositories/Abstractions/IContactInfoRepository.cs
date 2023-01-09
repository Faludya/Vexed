using Vexed.Models;

namespace Vexed.Repositories.Abstractions
{
    public interface IContactInfoRepository : IRepositoryBase<ContactInfo>
    {
        ContactInfo GetContactInfoById(int id);
    }
}