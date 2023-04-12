using Vexed.Data;
using Vexed.Repositories.Abstractions;

namespace Vexed.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private VexedDbContext _vexedDbContext;

        public RepositoryWrapper(VexedDbContext vexedDbContext)
        {
            _vexedDbContext = vexedDbContext;
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
                    _contactInfoRepository = new ContactInfoRepository(_vexedDbContext);
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
                    _emergencyContactRepository = new EmergencyContactRepository(_vexedDbContext);
                }
                return _emergencyContactRepository;
            }
        }

        private IUserDetailsRepository _userDetailsRepository;
        public IUserDetailsRepository UserDetailsRepository
        {
            get
            {
                if (_userDetailsRepository == null)
                {
                    _userDetailsRepository = new UserDetailsRepository(_vexedDbContext);
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
                    _userEmploymentRepository = new UserEmploymentRepository(_vexedDbContext);
                }
                return _userEmploymentRepository;
            }
        }

        private ILeaveRequestRepository _leaveRequestRepository;
        public ILeaveRequestRepository LeaveRequestRepository
        {
            get
            {
                if(_leaveRequestRepository == null)
                {
                    _leaveRequestRepository = new LeaveRequestRepository(_vexedDbContext);
                }
                return _leaveRequestRepository;
            }
        }

        private ITimeCardRepository _timeCardRepository;
        public ITimeCardRepository TimeCardRepository
        {
            get
            {
                if(_timeCardRepository == null)
                {
                    _timeCardRepository = new TimeCardRepository(_vexedDbContext);
                }
                return _timeCardRepository;
            }
        }

        private IUserRepository _userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_vexedDbContext);
                }
                return _userRepository;
            }
        }
    }
}
