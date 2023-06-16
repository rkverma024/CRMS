﻿using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Core.RepositoryInterface
{
    public interface IDashBordRepository
    {
        DashBordViewModel StatusCount();
        DashBordViewModel PriorityCount();
        DashBordViewModel TypeCount();
    }
}
