using CRMS.Core.RepositoryInterface;
using CRMS.Core.ServiceInterface;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Services.Services
{
    public class DashBordService : IDashBordService
    {
        IDashBordRepository dashBordRepository;

        public DashBordService(IDashBordRepository dashBordRepository)
        {
            this.dashBordRepository = dashBordRepository;
        }

        public DashBordViewModel GetChart()
        {
            return dashBordRepository.PriorityCount();
        }

        public DashBordViewModel GetTicketCount()
        {
           return dashBordRepository.StatusCount();
        }

        public DashBordViewModel GetTypeChart()
        {
            return dashBordRepository.TypeCount();
        }
    }
}
