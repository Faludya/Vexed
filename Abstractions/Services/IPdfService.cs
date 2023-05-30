using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.Services
{
    public interface IPdfService
    {
        public byte[] GenerateSalaryPdf(Salary salary);
    }
}
