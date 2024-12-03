using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ReportService.Infrastructure.Repositories;

namespace ReportService.Infrastructure.Factory
{
    public class ReportRepositoryFactory : IReportRepositoryFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ReportRepositoryFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IReportRepository Create()
        {
            return _serviceProvider.GetRequiredService<IReportRepository>();
        }

    }
}
