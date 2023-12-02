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
        private readonly Logger _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RepositoryWrapper(VexedDbContext vexedDbContext, Logger logger, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _vexedDbContext = vexedDbContext;
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;

            _contactInfoRepository = new ContactInfoRepository(_vexedDbContext, _logger);
            _emergencyContactRepository = new EmergencyContactRepository(_vexedDbContext, _logger);
            _leaveRequestRepository = new LeaveRequestRepository(_vexedDbContext, _logger);
            _projectRepository = new ProjectRepository(_vexedDbContext, _logger);
            _projectTeamRepository = new ProjectTeamRepository(_vexedDbContext, _logger);
            _qualificationRepository = new QualificationRepository(_vexedDbContext, _logger);
            _salaryRepository = new SalaryRepository(_vexedDbContext, _logger);
            _timeCardRepository = new TimeCardRepository(_vexedDbContext, _logger);
            _toDoRepository = new ToDoRepository(_vexedDbContext, _logger);
            _userDetailsRepository = new UserDetailsRepository(_vexedDbContext, _logger);
            _userEmploymentRepository = new UserEmploymentRepository(_vexedDbContext, _logger);
            _userRepository = new UserRepository(_vexedDbContext, _logger, _userManager, _roleManager);
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

        private IEmergencyContactRepository _emergencyContactRepository;
        public IEmergencyContactRepository EmergencyContactRepository
        {
            get
            {
                return _emergencyContactRepository;
            }
        }

        private ILeaveRequestRepository _leaveRequestRepository;
        public ILeaveRequestRepository LeaveRequestRepository
        {
            get
            {
                return _leaveRequestRepository;
            }
        }

        private IProjectRepository _projectRepository;
        public IProjectRepository ProjectRepository
        {
            get
            {
                return _projectRepository;
            }
        }

        private IProjectTeamRepository _projectTeamRepository;
        public IProjectTeamRepository ProjectTeamRepository
        {
            get
            {
                return _projectTeamRepository;
            }
        }

        private IQualificationRepository _qualificationRepository;
        public IQualificationRepository QualificationRepository
        {
            get
            {
                return _qualificationRepository;
            }
        }

        private ISalaryRepository _salaryRepository;
        public ISalaryRepository SalaryRepository
        {
            get
            {
                return _salaryRepository;
            }
        }
        private ITimeCardRepository _timeCardRepository;
        public ITimeCardRepository TimeCardRepository
        {
            get
            {
                return _timeCardRepository;
            }
        }

        private IToDoRepository _toDoRepository;
        public IToDoRepository ToDoRepository
        {
            get
            {
                return _toDoRepository;
            }
        }


        private IUserDetailsRepository _userDetailsRepository;
        public IUserDetailsRepository UserDetailsRepository
        {
            get
            {
                return _userDetailsRepository;
            }
        }

        private IUserEmploymentRepository _userEmploymentRepository;
        public IUserEmploymentRepository UserEmploymentRepository
        {
            get
            {
                return _userEmploymentRepository;
            }
        }

        private IUserRepository _userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                return _userRepository;
            }
        }
    }
}
