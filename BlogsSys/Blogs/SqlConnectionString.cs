using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Blogs
{
    public class SqlConnectionString
    {
        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
       
    }
}

