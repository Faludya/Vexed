﻿using Abstractions.Repositories;

namespace Vexed.Repositories.Abstractions
{
    public interface IRepositoryWrapper
    {
        IContactInfoRepository ContactInfoRepository { get; }
        IEmergencyContactRepository EmergencyContactRepository { get; }
        ILeaveRequestRepository LeaveRequestRepository { get; }
        IProjectRepository ProjectRepository{ get; }
        IProjectTeamRepository ProjectTeamRepository{ get; }
        IQualificationRepository QualificationRepository { get; }
        ISalaryRepository SalaryRepository{ get; }
        ITimeCardRepository TimeCardRepository { get; }
        IToDoRepository ToDoRepository{ get; }
        IUserDetailsRepository UserDetailsRepository { get; }
        IUserEmploymentRepository UserEmploymentRepository { get; }
        IUserRepository UserRepository { get; }
        Task Save();
    }
}
