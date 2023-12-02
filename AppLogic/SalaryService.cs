using Abstractions.Services;
using DataModels;
using Shared;
using Vexed.Repositories.Abstractions;

namespace AppLogic
{
    public class SalaryService : ISalaryService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly Logger _logger;

        public SalaryService(IRepositoryWrapper repositoryWrapper, Logger logger)
        {
            _repositoryWrapper = repositoryWrapper;
            _logger = logger;
        }

        public async Task CreateSalary(Salary salary)
        {
            try
            {
                await _repositoryWrapper.SalaryRepository.Create(salary);
                await _repositoryWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task DeleteSalary(Salary salary)
        {
            try
            {
                await _repositoryWrapper.SalaryRepository.Delete(salary);
                await _repositoryWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<Salary> GenerateSalary(Guid userId)
        {
            try
            {
                Salary salary = new Salary();
                var userEmployment = await _repositoryWrapper.UserEmploymentRepository.GetUserEmploymentByUserId(userId);
                var fullName = _repositoryWrapper.UserDetailsRepository.GetFullName(userId);
                //total hours  = time card + leave(!= Unpaid leave)
                var totalHours = _repositoryWrapper.TimeCardRepository.GetTotalWorkedHours(userId) +
                                    _repositoryWrapper.LeaveRequestRepository.GetLeaveHours(userId);
                var totalDays = _repositoryWrapper.TimeCardRepository.GetTotalWorkedDays(userId) +
                                    _repositoryWrapper.LeaveRequestRepository.GetLeaveDays(userId);
                var grossSalary = userEmployment.HourlyPay * totalHours;

                var socialInsurance = grossSalary * (salary.SocialInsuranceTax / 100);
                var healthInsurance = grossSalary * (salary.HealthInsuranceTax / 100);
                var personalIncome = grossSalary * (salary.PersonalIncomeTax / 100);
                var workInsurance = grossSalary * (salary.WorkInsuranceTax / 100);

                var netSalary = grossSalary - socialInsurance - healthInsurance - personalIncome - workInsurance;
                var mealTicketsTotal = salary.MealTicketValue * totalDays;

                salary.UserId = userId;
                salary.FullName = fullName;
                salary.Function = userEmployment.Function;
                salary.Department = userEmployment.Department;
                salary.Company = userEmployment.CompanyName;
                salary.GeneratedDate = DateTime.Now;
                salary.TotalWorkedHours = totalHours;
                salary.TotalWorkedDays = totalDays;
                salary.GrossSalary = grossSalary;

                salary.SocialInsuranceValue = socialInsurance;
                salary.HealthInsuranceValue = healthInsurance;
                salary.PersonalIncomeValue = personalIncome;
                salary.WorkInsuranceValue = workInsurance;

                salary.NetSalary = netSalary;
                salary.MealTicketTotal = mealTicketsTotal;
                salary.Status = SalaryStatusManager.Generated;

                return salary;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<List<Salary>> GetSalaries(DateTime dateTime)
        {
            try
            {
                return await _repositoryWrapper.SalaryRepository.GetSalaries(dateTime);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task<Salary> GetSalaryById(int id)
        {
            try
            {
                return await _repositoryWrapper.SalaryRepository.GetSalaryById(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        public async Task UpdateSalary(Salary salary)
        {
            try
            {
                await _repositoryWrapper.SalaryRepository.Update(salary);
                await _repositoryWrapper.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
