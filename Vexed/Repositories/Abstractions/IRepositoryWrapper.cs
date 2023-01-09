namespace Vexed.Repositories.Abstractions
{
    public interface IRepositoryWrapper
    {
        IContactInfoRepository ContactInfoRepository { get; }
        IEmergencyContactRepository EmergencyContactRepository { get; }
        IUserDetailsRepository UserDetailsRepository { get; }
        IUserEmploymentRepository UserEmploymentRepository { get; }

        void Save();
    }
}
