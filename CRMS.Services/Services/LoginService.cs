using CRMS.Core.Contracts;
using CRMS.Core.Models;
using CRMS.DataAccess.SQL;
using CRMS.WebUI.Models;
using Scrypt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMS.Services
{
    public class LoginService
    {
        LoginRepository loginrepository;
        public LoginService(LoginRepository loginRepository)
        {
            this.loginrepository = loginRepository;
        }

        public User Login(LoginViewModel model)
        {
            
            User user = loginrepository.Login(model.Email);           
            return user;
        }

    }
}
