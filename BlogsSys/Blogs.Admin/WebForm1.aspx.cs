using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Blogs.DAL;

namespace Blogs.Admin
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string path = HttpContext.Current.Server.MapPath("~/"); //Blogs.DAL/DB/BlogsDB.db3");
            Response.Write(path);

        }
    }
}