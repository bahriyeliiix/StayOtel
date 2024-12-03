using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReportService.Infrastructure.Repositories;

namespace ReportService.Infrastructure.Factory
{
    public interface IReportRepositoryFactory
    {
        IReportRepository Create();
    }
}
