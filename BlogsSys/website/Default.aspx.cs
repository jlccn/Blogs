using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Blogs.DAL;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        using (SysEntities db = new SysEntities())
        {
            foreach (var item in db.SysPerson)
                Response.Write(item.Name);

        }
    }
}