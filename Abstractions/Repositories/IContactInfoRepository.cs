using DataModels;

namespace Vexed.Repositories.Abstractions
{
    public interface IContactInfoRepository : IRepositoryBase<ContactInfo>
    {
        /// <summary>
        /// Returns the first Contact Info with the given <c>id</c>
        /// </summary>
        Task<ContactInfo> GetContactInfoById(int id);

        /// <summary>
        /// Returns all the Contact Infos for a given user. 
        /// </summary>
        Task<List<ContactInfo>> GetContactInfos(Guid userId);
    }
}