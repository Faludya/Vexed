using DataModels;

namespace Abstractions.Services
{
    public interface ISalaryService
    {
        /// <summary>
        /// Creates a new Salary.
        /// </summary>
        Task CreateSalary(Salary salary);

        /// <summary>
        /// Updates the details of a Salary
        /// </summary>
        Task UpdateSalary(Salary salary);

        /// <summary>
        /// Removes the Salary from the database.
        /// </summary>
        Task DeleteSalary(Salary salary);

        /// <summary>
        /// Returns the first Salary with the given <c>id</c>
        /// </summary>
        Task<Salary> GetSalaryById(int id);

        /// <summary>
        /// Generates the Salary for the user witht the <c>userId</c>
        /// </summary>
        Task<Salary> GenerateSalary(Guid userId);

        /// <summary>
        /// Generates the list of salaries for the given month <c>userId</c>
        /// </summary>
        Task<List<Salary>> GetSalaries(DateTime dateTime);
    }
}
