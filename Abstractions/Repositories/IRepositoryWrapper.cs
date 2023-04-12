namespace Vexed.Repositories.Abstractions
{
    public interface IRepositoryWrapper
    {
        IContactInfoRepository ContactInfoRepository { get; }
        IEmergencyContactRepository EmergencyContactRepository { get; }
        IUserDetailsRepository UserDetailsRepository { get; }
        IUserEmploymentRepository UserEmploymentRepository { get; }
        ILeaveRequestRepository LeaveRequestRepository { get; }
        ITimeCardRepository TimeCardRepository { get; }
        IUserRepository UserRepository { get; }
        Task Save();
    }
}
