﻿using CRMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.DataAccess.SQL
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("CRMS")
        {
        }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<ConferenceRoom> ConferenceRooms { get; set; }
        public DbSet<CommonLookUp> CommonLookUps { get; set; }
        public DbSet<FormMst> FormMsts { get; set; }
        public DbSet<FormRoleMapping> FormRoleMappings { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketAttachment> TicketAttachments { get; set; }
        public DbSet<TicketComment> TicketComments { get; set; }
        public DbSet<TicketStatusHistory> TicketStatusHistorys { get; set; }
        public DbSet<AuditLogs> AuditLog { get; set; }
    }
}
