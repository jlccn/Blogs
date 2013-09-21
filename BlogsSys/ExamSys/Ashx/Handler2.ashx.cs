using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DapperExtensions;

namespace ExamSys
{
    /// <summary>
    /// Handler2 的摘要说明
    /// </summary>
    public class Handler2 : System.Web.WebHandler
    {
        IDatabase Db = DB.GetDatabase();
        [ResponseAnnotation(Desc = "取得分页需要的数据源")]
        public object GetPageData(int page, int rows, string key)
        {
            int pageIndex = page - 1;
            int pageSzie = rows;

            IPredicate pred = null;
            if (!string.IsNullOrEmpty(key))
            {
                key = string.Format("%{0}%", key);
                pred = Predicates.Field<Archive>(p => p.Content, Operator.Like, key);
            }

            var sort = new List<ISort>
                                    {
                                        Predicates.Sort<Archive>(p => p.Id)
                                    };

            List<Archive> List = Db.GetPage<Archive>(pred, sort, pageIndex, pageSzie).ToList();
            foreach (Archive arc in List)
                arc.Content = arc.Content;
            int count = Db.Count<Archive>(pred);

            var obj = new { total = count, rows = List };
            return obj;
        }
        
        [ResponseAnnotation(Desc = "删除一条数据")]
        public string DeleteByID(int id)
        {
            var result = Db.Delete<Archive>(new Archive { Id = id });
            return result ? "OK" : "ERROR";
        }
        [ResponseAnnotation(Desc = "更新一条数据")]
        public string UpdateByID(int id)
        {
            var m = Db.Get<Archive>(id);
            m.Subject = GetFormValue("Subject");
            m.Content = GetFormValue("Content");

            var result = Db.Update(m);
            return result ? "OK" : "ERROR";
        }

        private string GetFormValue(string name)
        {
            return HttpContext.Current.Request.Form[name].ToString();
        }

        public string Add()
        {
            Archive arc = new Archive()
            {
                Subject = GetFormValue("Subject"),
                Content = GetFormValue("Content")
            };
            int result = Db.Insert<Archive>(arc);
            return result > 0 ? "OK" : "ERROR";
        }
    }
}