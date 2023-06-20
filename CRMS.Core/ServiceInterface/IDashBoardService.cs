using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.ServiceInterface
{
    public interface IDashBoardService
    {
        DashBoardViewModel GetTicketCount();

        DashBoardViewModel GetChart();
        DashBoardViewModel GetTypeChart();
        DashBoardViewModel GetTicketChart();
        DashBoardViewModel GetUserChart();
    }
}
