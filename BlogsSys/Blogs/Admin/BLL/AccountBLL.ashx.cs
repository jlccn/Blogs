using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using DapperExtensions;
using Blogs.DAL;
using Blogs.Model;
using Blogs.Common;
using System.Text;
using Dapper;

namespace Blogs.Admin.BLL
{
    /// <summary>
    /// AccountBLL 的摘要说明
    /// </summary>
    public class AccountBLL :  System.Web.WebHandler, IRequiresSessionState
    {
        IDatabase Db = DBHelper.GetDatabase();

        public string CheckLogin(string userName, string password)
        {
            string passwordDes = EncryptAndDecrypte.EncryptString(password);                       
            IPredicateGroup predGrp = null;
            var pred1 = Predicates.Field<SysPerson>(p => p.Name, Operator.Eq, userName);
            var pred2 = Predicates.Field<SysPerson>(p => p.Password, Operator.Eq, passwordDes);
            predGrp = Predicates.Group(GroupOperator.And, pred1, pred2);
            var person = Db.GetList<SysPerson>(predGrp).SingleOrDefault();       
            if (person != null)
            {
                Account account = new Account()
                {
                    Name = person.MyName,
                    PersonName = person.Name,
                    Id = person.Id                    
                };
                Session["account"] = account;
                //Session["userName"] = person.MyName;
                return "1";
            }
            else
                return "0";     
        }

        public string IsLogin()
        {
            if (Session["account"] == null)
                return "location.href='login.htm'";
            else
                return "";
        }

        public string LoginOut()
        {
            if (Session["account"] != null)
                Session["account"] = null;
            return "location.href='login.htm'";
        }
              

        public Account GetCurrentAccount()
        {
            if (Session["account"] != null)
            {
                Account account = (Account)Session["account"];
                return account;
            }
            return null;
        }

        public string GetMenuByAccount()
        {
            Account account = GetCurrentAccount();
            return GetMenuByAccount(ref account);
        }

        /// <summary>
        /// 根据PersonId获取已经启用的菜单
        /// </summary>
        /// <param name="personId">人员的Id</param>
        /// <returns>菜单拼接的字符串</returns>
        public string GetMenuByAccount(ref Common.Account account)
        {
            string personId = account.Id;
            var pred1 = Predicates.Field<SysRoleSysPerson>(p => p.SysPersonId, Operator.Eq, personId);
            foreach (var item in Db.GetList<SysRoleSysPerson>(pred1))
            {
                account.RoleIds.Add(item.SysRoleId);
            }



            return personId; //待改
        }

        /// <summary>
        /// 获取节点的字符串
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetNode(SysMenu item, bool isLeaf = false)
        {
            List<string> dataoptions = new List<string>();
            if (!string.IsNullOrWhiteSpace(item.Iconic))
            {
                dataoptions.Add(string.Format("iconCls:'{0}'", item.Iconic));
            }

            if (isLeaf)
            {
                return string.Format("<li data-options=@{0}@><a href=@#@ icon=@{1}@ rel=@{2}@ id=@{3}@>{4}</a></li>^", string.Join(",", dataoptions), item.Iconic, item.Url, item.Id, item.Name);

            }
            else
            {
                ////此处可以在数据字典中配置，将State字段，配置为下拉框，下拉框其中有一个值是"收缩"
                if (!string.IsNullOrWhiteSpace(item.State))//收缩展开 && item.State == "收缩"
                {//菜单默认显示关闭
                    dataoptions.Add(string.Format("state:'closed'"));
                }
                return string.Format("<li data-options=@{0}@><span>{1}</span><ul>^</ul></li>", string.Join(",", dataoptions), item.Name);
            }
        }

    }
}