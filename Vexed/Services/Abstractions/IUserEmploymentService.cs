using Vexed.Models;

namespace Vexed.Services.Abstractions
{
    public interface IUserEmploymentService
    {
        void CreateUserEmployment(UserEmployment userEmployment);
        void UpdateUserEmployment(UserEmployment userEmployment);
        void DeleteUserEmployment(UserEmployment userEmployment);
        UserEmployment GetUserEmploymentById(int id);
        List<UserEmployment> GetAllUsersEmployment();
    }
}
