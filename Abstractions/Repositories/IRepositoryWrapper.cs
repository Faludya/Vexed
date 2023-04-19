using Abstractions.Repositories;

namespace Vexed.Repositories.Abstractions
{
    public interface IRepositoryWrapper
    {
        IContactInfoRepository ContactInfoRepository { get; }
        IEmergencyContactRepository EmergencyContactRepository { get; }
        ILeaveRequestRepository LeaveRequestRepository { get; }
        IQualificationRepository QualificationRepository { get; }
        ITimeCardRepository TimeCardRepository { get; }
        IUserDetailsRepository UserDetailsRepository { get; }
        IUserEmploymentRepository UserEmploymentRepository { get; }
        IUserRepository UserRepository { get; }
        Task Save();
    }
}
