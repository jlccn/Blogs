using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using DapperExtensions;
using Blogs.DAL;

namespace Blogs.Business
{
    /// <summary>
    /// UserBLL_Ad 的摘要说明
    /// </summary>
    public class UserBLL_Ad : System.Web.WebHandler, IRequiresSessionState
    {
        IDatabase Db = DB.GetDatabase();

        [ResponseAnnotation(Desc = "验证登录用户信息")]
        public string CheckLogin(string userName, string password)
        {
            if (userName == "gdjlc" && password == "gdjlcgdjlc")
            {
                Session["userName"] = userName;
                return "1";
            }
            else
                return "0";
        }

        public string IsLogin()
        {
            if (Session["userName"] == null)
                return "location.href='login.htm'"; 
            else
                return "";
        }

        public string LoginOut()
        {
            if (Session["userName"] != null)
                Session["userName"] = null;
             return "location.href='login.htm'";            
        }

        public string GetUser()
        {
            if (Session["userName"] != null)
                return Session["userName"].ToString();
            else
                return ""; 
        }
    }
}