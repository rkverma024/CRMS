﻿using CRMS.Core.Contracts;
using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.DataAccess.SQL
{
    public class TicketRepository : ITicketRepository
    {
        internal DataContext context;
        internal DbSet<Ticket> dbSet;
        public TicketRepository(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<Ticket>();
        }
        public IQueryable<Ticket> Collection()
        {
            return dbSet;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void Delete(Guid Id)
        {
            var ticket = Find(Id);
            if (context.Entry(ticket).State == EntityState.Detached)
            {
                dbSet.Attach(ticket);
            }
            dbSet.Remove(ticket);
        }

        public Ticket Find(Guid Id)
        {
            return dbSet.Find(Id);
        }

        public void Insert(Ticket model)
        {
            dbSet.Add(model);
        }

        public void Update(Ticket model)
        {
            dbSet.Attach(model);
            context.Entry(model).State = EntityState.Modified;
        }
        public IEnumerable<TicketIndexViewModel> AllTicketList()
        {
            var list = from tk in context.Tickets.Where(x => x.IsDeleted == false)
                       join user in context.Users.Where(x => x.IsDeleted == false) on tk.AssignTo equals user.Id
                       join clup in context.CommonLookUps.Where(x => x.IsDeleted == false) on tk.TypeId equals clup.Id
                       join clm in context.CommonLookUps.Where(x => x.IsDeleted == false) on tk.PriorityId equals clm.Id
                       join clp in context.CommonLookUps.Where(x => x.IsDeleted == false) on tk.StatusId equals clp.Id                      
                      /* join ticketAttachment in context.TicketAttachments.Where(x => x.IsDeleted == false) on tk.Id 
                       equals ticketAttachment.TicketId into tattach from ti in tattach.DefaultIfEmpty()*/
                       select new TicketIndexViewModel()
                       {
                           Id =  tk.Id,
                           Title = tk.Title,                           
                           AssignTo = user.Name,                           
                           TypeId = clup.ConfigValue,
                           PriorityId = clm.ConfigValue,
                           StatusId = clp.ConfigValue,                           
                           Description = tk.Description,
                           StatusDropDown = (from c in context.CommonLookUps.Where(x => x.IsDeleted == false && x.ConfigName == "Status")
                                             select new DropDown()
                                             {
                                                 Id = c.Id,
                                                 Name = c.ConfigValue
                                             }).ToList(),
                           FileName = context.TicketAttachments.Where(x => x.TicketId == tk.Id && x.IsDeleted == false).Count()
                           //FileName = ti == null ? "" : ti.FileName

                       };
            return list;
        }
    }
}