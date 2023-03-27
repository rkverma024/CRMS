using CRMS.Core.Contracts;
using CRMS.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Services
{
    public class LoginService : ILoginService
    {
        IRepository<User> repository;
        public LoginService(IRepository<User> loginRepository)
        {
            this.repository = loginRepository;
        }
        /*public User Login(string email)
        {
            var validUser = (from u in repository.User where u.Email.Equals(email) select u).FirstOrDefault();
            return validUser;
        }*/
    }
}
