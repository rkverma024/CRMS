using CRMS.Core.Models;
using CRMS.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrypt;
namespace CRMS.DataAccess.SQL
{
    public class LoginRepository 
    {
        internal DataContext context;

        public LoginRepository(DataContext context)
        {
            this.context = context;
            
        }
      

        public User Login(string email)
        {
            var validUser = (from u in context.Users where u.Email.Equals(email) select u).FirstOrDefault();
            return validUser;
            /*var user = context.Users.Where(a => a.Email == model.Email && a.Password == model.Password).Count();
            return user;*/
        }
    }
}
