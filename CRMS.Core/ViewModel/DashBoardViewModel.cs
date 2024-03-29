﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.ViewModel
{
    public class DashBoardViewModel
    {
        public int TotalCount { get; set; }
        public int NewCount { get; set; }
        public int PendingCount { get; set; }
        public int ResolvedCount { get; set; }
        
        public List<ChartViewModel> ChartData { get; set; }

        public List<TypeViewModel> TypeChartData { get; set; }

        public List<TicketsChartViewModel> TicketChartData { get; set; }

        public List<UserChartViewModel> UserChartData { get; set; }
    }
}
