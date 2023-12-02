using Shared;
using DataAccess;
using Vexed.Repositories.Abstractions;
using Abstractions.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Vexed.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly VexedDbContext _vexedDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RepositoryWrapper(VexedDbContext vexedDbContext, Logger logger, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _vexedDbContext = vexedDbContext;
            _userManager = userManager;
            _roleManager = roleManager;

            _contactInfoRepository = new ContactInfoRepository(_vexedDbContext, logger);
            _emergencyContactRepository = new EmergencyContactRepository(_vexedDbContext, logger);
            _leaveRequestRepository = new LeaveRequestRepository(_vexedDbContext, logger);
            _projectRepository = new ProjectRepository(_vexedDbContext, logger);
            _projectTeamRepository = new ProjectTeamRepository(_vexedDbContext, logger);
            _qualificationRepository = new QualificationRepository(_vexedDbContext, logger);
            _salaryRepository = new SalaryRepository(_vexedDbContext, logger);
            _timeCardRepository = new TimeCardRepository(_vexedDbContext, logger);
            _toDoRepository = new ToDoRepository(_vexedDbContext, logger);
            _userDetailsRepository = new UserDetailsRepository(_vexedDbContext, logger);
            _userEmploymentRepository = new UserEmploymentRepository(_vexedDbContext, logger);
            _userRepository = new UserRepository(_vexedDbContext, logger, _userManager, _roleManager);
        }

        public async Task Save()
        {
            await _vexedDbContext.SaveChangesAsync();
        }

        private readonly IContactInfoRepository _contactInfoRepository;
        public IContactInfoRepository ContactInfoRepository
        {
            get
            {
                return _contactInfoRepository;
            }
        }

        private readonly IEmergencyContactRepository _emergencyContactRepository;
        public IEmergencyContactRepository EmergencyContactRepository
        {
            get
            {
                return _emergencyContactRepository;
            }
        }

        private readonly ILeaveRequestRepository _leaveRequestRepository;
        public ILeaveRequestRepository LeaveRequestRepository
        {
            get
            {
                return _leaveRequestRepository;
            }
        }

        private readonly IProjectRepository _projectRepository;
        public IProjectRepository ProjectRepository
        {
            get
            {
                return _projectRepository;
            }
        }

        private readonly IProjectTeamRepository _projectTeamRepository;
        public IProjectTeamRepository ProjectTeamRepository
        {
            get
            {
                return _projectTeamRepository;
            }
        }

        private readonly IQualificationRepository _qualificationRepository;
        public IQualificationRepository QualificationRepository
        {
            get
            {
                return _qualificationRepository;
            }
        }

        private readonly ISalaryRepository _salaryRepository;
        public ISalaryRepository SalaryRepository
        {
            get
            {
                return _salaryRepository;
            }
        }
        private readonly ITimeCardRepository _timeCardRepository;
        public ITimeCardRepository TimeCardRepository
        {
            get
            {
                return _timeCardRepository;
            }
        }

        private readonly IToDoRepository _toDoRepository;
        public IToDoRepository ToDoRepository
        {
            get
            {
                return _toDoRepository;
            }
        }


        private readonly IUserDetailsRepository _userDetailsRepository;
        public IUserDetailsRepository UserDetailsRepository
        {
            get
            {
                return _userDetailsRepository;
            }
        }

        private readonly IUserEmploymentRepository _userEmploymentRepository;
        public IUserEmploymentRepository UserEmploymentRepository
        {
            get
            {
                return _userEmploymentRepository;
            }
        }

        private readonly IUserRepository _userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                return _userRepository;
            }
        }
    }
}
