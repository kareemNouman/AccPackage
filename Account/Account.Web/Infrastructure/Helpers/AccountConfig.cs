using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Account.Web.Infrastructure.Helpers
{
    public class AccountConfig
    {
        public static string ApiUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["ApiUrl"];
            }
        }


        public static string ApiEndPoint
        {
            get
            {
                return ConfigurationManager.AppSettings["apiEndPoint"];
            }
        }


    }
}