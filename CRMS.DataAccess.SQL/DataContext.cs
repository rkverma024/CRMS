using CRMS.Core.Models;
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
            :base("CRMS"){
        }           
        public DbSet<BaseEntity> BaseEntity { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> User_Roles { get; set; }



    }
}
