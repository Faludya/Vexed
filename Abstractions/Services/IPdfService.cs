using DataModels;

namespace Abstractions.Services
{
    public interface IPdfService
    {
        public byte[] GenerateSalaryPdf(Salary salary);
    }
}
