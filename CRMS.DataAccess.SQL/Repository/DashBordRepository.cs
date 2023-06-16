using CRMS.Core.Models;
using CRMS.Core.RepositoryInterface;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.DataAccess.SQL.Repository
{
    public class DashBordRepository : IDashBordRepository
    {
        internal DataContext context;
        internal DbSet<Ticket> dbSet;
        public DashBordRepository(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<Ticket>();
        }

        public DashBordViewModel PriorityCount()
        {
            var viewModel = new DashBordViewModel();
            List<ChartViewModel> chartviewmodel = (from tc in context.Tickets
                                          join com in context.CommonLookUps on tc.PriorityId equals com.Id
                                          select new ChartViewModel
                                          {
                                              category = com.ConfigValue
                                          }).ToList();
            viewModel.ChartData = new List<ChartViewModel>();
            viewModel.ChartData.Add(new ChartViewModel
            {
                value = chartviewmodel.Where(x => x.category == "High").Count(),
                category = "High"
            });

            viewModel.ChartData.Add(new ChartViewModel
            {
                value = chartviewmodel.Where(x => x.category == "Low").Count(),
                category = "Low"
            });

            viewModel.ChartData.Add(new ChartViewModel
            {
                value = chartviewmodel.Where(x => x.category == "Medium").Count(),
                category = "Medium"
            });
            viewModel.ChartData.Add(new ChartViewModel
            {
                value = chartviewmodel.Where(x => x.category == "Immediate").Count(),
                category = "Immediate"
            });

            return viewModel;
        }

        public DashBordViewModel StatusCount()
        {
            DashBordViewModel viewmodel = new DashBordViewModel();
            viewmodel.TotalCount = (from tc in context.Tickets where !tc.IsDeleted select tc).Count();

            viewmodel.NewCount = (from tc in context.Tickets 
                                  join com in context.CommonLookUps on tc.StatusId equals com.Id 
                                  where com.ConfigValue == "New"  select tc).Count();

            viewmodel.PendingCount = (from tc in context.Tickets
                                  join com in context.CommonLookUps on tc.StatusId equals com.Id
                                  where com.ConfigValue == "Pending" select tc).Count();

            viewmodel.ResolvedCount = (from tc in context.Tickets
                                  join com in context.CommonLookUps on tc.StatusId equals com.Id
                                  where com.ConfigValue == "Resolved"
                                     select tc).Count();

            return viewmodel;
        }

        public DashBordViewModel TypeCount()
        {
            var viewModel = new DashBordViewModel();
            List<TypeViewModel> typeviewmodel = (from tc in context.Tickets
                                                   join com in context.CommonLookUps on tc.TypeId equals com.Id
                                                   select new TypeViewModel
                                                   {
                                                       category = com.ConfigValue
                                                   }).ToList();
            viewModel.TypeChartData = new List<TypeViewModel>();
            viewModel.TypeChartData.Add(new TypeViewModel
            {
                value = typeviewmodel.Where(x => x.category == "New-Requirement").Count(),
                category = "New-Requirement"
            });

            viewModel.TypeChartData.Add(new TypeViewModel
            {
                value = typeviewmodel.Where(x => x.category == "Defect").Count(),
                category = "Defect"
            });

            viewModel.TypeChartData.Add(new TypeViewModel
            {
                value = typeviewmodel.Where(x => x.category == "Enhancement").Count(),
                category = "Enhancement"
            });
            viewModel.TypeChartData.Add(new TypeViewModel
            {
                value = typeviewmodel.Where(x => x.category == "Bug").Count(),
                category = "Bug"
            });
            viewModel.TypeChartData.Add(new TypeViewModel
            {
                value = typeviewmodel.Where(x => x.category == "Finetuning").Count(),
                category = "Finetuning"
            });

            return viewModel;
        }
    }
}
