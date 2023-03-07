using CRMS.Core.Contracts;
using CRMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.DataAccess.SQL
{
    public class SqlRepository
    {
        internal DataContext context;
        /* internal DbSet<T>*/

        public SqlRepository()
        {

            //this.context = context;
            context = new DataContext();
        }


        /*public SqlRepository(DataContext context)
        {
            this.context = context;
            this.dbSet = 
        }*/



        public IQueryable<Core.Models.User> LoginRepository()
        {
            var user = context.Users.Where(x => x.Role == "Admin");
            return user;           
        }


       
    }
}
