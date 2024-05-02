using System;
using System.Collections.Generic;
using System.Configuration;

namespace ShaApplication.Utility
{
    public static class WebHelper
    {
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["SHAConnectionString"].ConnectionString;
            }
        }
    }
}