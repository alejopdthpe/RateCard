using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RateCard.Api.Core
{
    public class AdministrationManagement
    {
        private static AdministrationManagement _instance;

        private string userName = "";
        private string userPassword = "";

        public static AdministrationManagement GetInstance()
        {
            return _instance ?? (_instance = new AdministrationManagement());
        }
        private AdministrationManagement()
        {

        }

        public void SetUserCredentials(string pemail,string ppassword)
        {
            userName = pemail;
            userPassword = ppassword;
        }
    }
}
