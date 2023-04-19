using Shared;
using DataAccess;
using Vexed.Repositories.Abstractions;
using Abstractions.Repositories;

namespace Vexed.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private VexedDbContext _vexedDbContext;
        private Logger _logger;

        public RepositoryWrapper(VexedDbContext vexedDbContext, Logger logger)
        {
            _vexedDbContext = vexedDbContext;
            _logger = logger;
        }

        public async Task Save()
        {
            await _vexedDbContext.SaveChangesAsync();
        }

        private IContactInfoRepository _contactInfoRepository;
        public IContactInfoRepository ContactInfoRepository
        {
            get
            {
                if (_contactInfoRepository == null)
                {
                    _contactInfoRepository = new ContactInfoRepository(_vexedDbContext, _logger);
                }
                return _contactInfoRepository;
            }
        }

        private IEmergencyContactRepository _emergencyContactRepository;
        public IEmergencyContactRepository EmergencyContactRepository
        {
            get
            {
                if (_emergencyContactRepository == null)
                {
                    _emergencyContactRepository = new EmergencyContactRepository(_vexedDbContext, _logger);
                }
                return _emergencyContactRepository;
            }
        }

        private ILeaveRequestRepository _leaveRequestRepository;
        public ILeaveRequestRepository LeaveRequestRepository
        {
            get
            {
                if (_leaveRequestRepository == null)
                {
                    _leaveRequestRepository = new LeaveRequestRepository(_vexedDbContext, _logger);
                }
                return _leaveRequestRepository;
            }
        }

        private IQualificationRepository _qualificationRepository;
        public IQualificationRepository QualificationRepository
        {
            get
            {
                if(_qualificationRepository == null)
                {
                    _qualificationRepository = new QualificationRepository(_vexedDbContext, _logger);
                }
                return _qualificationRepository;
            }
        }

        private ITimeCardRepository _timeCardRepository;
        public ITimeCardRepository TimeCardRepository
        {
            get
            {
                if (_timeCardRepository == null)
                {
                    _timeCardRepository = new TimeCardRepository(_vexedDbContext, _logger);
                }
                return _timeCardRepository;
            }
        }


        private IUserDetailsRepository _userDetailsRepository;
        public IUserDetailsRepository UserDetailsRepository
        {
            get
            {
                if (_userDetailsRepository == null)
                {
                    _userDetailsRepository = new UserDetailsRepository(_vexedDbContext, _logger);
                }
                return _userDetailsRepository;
            }
        }

        private IUserEmploymentRepository _userEmploymentRepository;
        public IUserEmploymentRepository UserEmploymentRepository
        {
            get
            {
                if (_userDetailsRepository == null)
                {
                    _userEmploymentRepository = new UserEmploymentRepository(_vexedDbContext, _logger);
                }
                return _userEmploymentRepository;
            }
        }

        private IUserRepository _userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_vexedDbContext, _logger);
                }
                return _userRepository;
            }
        }
    }
}
