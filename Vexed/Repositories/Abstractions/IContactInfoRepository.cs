using Vexed.Models;

namespace Vexed.Repositories.Abstractions
{
    public interface IContactInfoRepository : IRepositoryBase<ContactInfo>
    {
        /// <summary>
        /// Returns the first Contact Info with the given <c>id</c>
        /// </summary>
        ContactInfo GetContactInfoById(int id);

        /// <summary>
        /// Returns all the Contact Infos for a given user. 
        /// </summary>
        List<ContactInfo> GetContactInfos(Guid userId);
    }
}