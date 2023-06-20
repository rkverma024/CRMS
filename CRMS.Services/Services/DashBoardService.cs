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
    public class DashBoardService : IDashBoardService
    {
        IDashBoardRepository DashBoardRepository;

        public DashBoardService(IDashBoardRepository DashBoardRepository)
        {
            this.DashBoardRepository = DashBoardRepository;
        }

        public DashBoardViewModel GetChart()
        {
            return DashBoardRepository.PriorityCount();
        }

        public DashBoardViewModel GetTicketCount()
        {
           return DashBoardRepository.StatusCount();
        }

        public DashBoardViewModel GetTypeChart()
        {
            return DashBoardRepository.TypeCount();
        }

        public DashBoardViewModel GetTicketChart()
        {
            return DashBoardRepository.TicketsCount();
        }
        public DashBoardViewModel GetUserChart()
        {
            return DashBoardRepository.UserCount();
        }
    }
}
