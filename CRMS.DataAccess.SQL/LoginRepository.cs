using CRMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.DataAccess.SQL
{
    public class LoginRepository
    {
        internal DataContext context;
        
        public LoginRepository()
        {

            //this.context = context;
            context = new DataContext();
        }
        public IQueryable<User> loginRepository()
        {
            var user = context.Users.Where(x => x.Role == "Admin");
            return user;
        }
    }
}
