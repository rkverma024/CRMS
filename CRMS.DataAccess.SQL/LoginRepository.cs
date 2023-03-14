using CRMS.Core.Models;
using CRMS.Core.ViewModel;
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
        /*public IQueryable<UserRole> loginRepository()
        {
            var user = context.Users.Where(x => x.RoleName == "Admin");
            return user;
        }*/

        public int Login(User model)
        {
            var user = context.Users.Where(a => a.Email == model.Email && a.Password == model.Password).Count();
            return user;
        }
    }
}
