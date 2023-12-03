using Abstractions.Services;
using DataModels;
using DataModels.ViewModels;
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
                Salary salary = new();
                var userEmployment = await _repositoryWrapper.UserEmploymentRepository.GetUserEmploymentByUserId(userId);
                if (userEmployment == null)
                {
                    return salary;
                }

                var fullName = _repositoryWrapper.UserDetailsRepository.GetFullName(userId);
                var totalHours = _repositoryWrapper.TimeCardRepository.GetTotalWorkedHours(userId) +
                                    _repositoryWrapper.LeaveRequestRepository.GetLeaveHours(userId);
                var totalDays = _repositoryWrapper.TimeCardRepository.GetTotalWorkedDays(userId) +
                                    _repositoryWrapper.LeaveRequestRepository.GetLeaveDays(userId);
                var grossSalary = userEmployment.HourlyPay * totalHours;

                var socialInsurance = CalculateSocialInsurance(grossSalary, salary.SocialInsuranceTax);
                var healthInsurance = CalculateHealthInsurance(grossSalary, salary.HealthInsuranceTax);
                var personalIncome = CalculatePersonalIncome(grossSalary, salary.PersonalIncomeTax);
                var workInsurance = CalculateWorkIncome(grossSalary, salary.WorkInsuranceTax);

                var netSalary = grossSalary - socialInsurance - healthInsurance - personalIncome - workInsurance;
                var mealTicketsTotal = salary.MealTicketValue * totalDays;
                PopulateSalaryData(userId, salary, userEmployment, fullName, totalHours, totalDays, grossSalary);
                PopulateSalaryTaxesData(salary, socialInsurance, healthInsurance, personalIncome, workInsurance, netSalary, mealTicketsTotal);

                return salary;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        private static void PopulateSalaryData(Guid userId, Salary salary, UserEmployment userEmployment, string fullName, float totalHours, int totalDays, float grossSalary)
        {
            salary.UserId = userId;
            salary.FullName = fullName;
            salary.Function = userEmployment.Function;
            salary.Department = userEmployment.Department;
            salary.Company = userEmployment.CompanyName;
            salary.GeneratedDate = DateTime.Now;
            salary.TotalWorkedHours = totalHours;
            salary.TotalWorkedDays = totalDays;
            salary.GrossSalary = grossSalary;
        }

        public static void PopulateSalaryTaxesData(Salary salary, float socialInsurance, float healthInsurance, float personalIncome, float workInsurance, float netSalary, int mealTicketsTotal)
        {
            salary.SocialInsuranceValue = socialInsurance;
            salary.HealthInsuranceValue = healthInsurance;
            salary.PersonalIncomeValue = personalIncome;
            salary.WorkInsuranceValue = workInsurance;

            salary.NetSalary = netSalary;
            salary.MealTicketTotal = mealTicketsTotal;
            salary.Status = SalaryStatusManager.Generated;
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
    
        public static float CalculateSocialInsurance(float grossSalary, float socialInsuranceTax)
        {
            return grossSalary * (socialInsuranceTax / 100);
        }

        public static float CalculateHealthInsurance(float grossSalary, float healthInsuranceTax)
        {
            return grossSalary * (healthInsuranceTax / 100);
        }

        public static float CalculatePersonalIncome(float grossSalary, float personalIncomeTax)
        {
            return grossSalary * (personalIncomeTax / 100);
        }

        public static float CalculateWorkIncome(float grossSalary, float workInsuranceTax)
        {
            return grossSalary * (workInsuranceTax / 100);
        }
    }
}
