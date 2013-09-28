using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DapperExtensions;
using Blogs.DAL;

namespace Blogs.Admin
{
    /// <summary>
    /// ArticleBLL_Ad 的摘要说明
    /// </summary>
    public class ArticleBLL : System.Web.WebHandler
    {

        IDatabase Db = DBHelper.GetDatabase();

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
                                        Predicates.Sort<Archive>(p => p.PublishDate, false)
                                    };

            var List = Db.GetPage<Archive>(pred, sort, pageIndex, pageSzie).ToList();         
            int count = Db.Count<Archive>(pred);

            var obj = new { total = count, rows = List };
            return obj;
        }

        public object GetArticleByID(int id)
        {
            var result = Db.Get<Archive>(new Archive { Id = id });
            return result;
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
            string publishDate = GetFormValue("PublishDate");
            var m = Db.Get<Archive>(id);
            m.Subject = GetFormValue("Subject");
            m.Content = GetFormValue("Content");
            m.CategoryId = Convert.ToInt32(GetFormValue("CategoryId"));
            m.PublishDate = string.IsNullOrEmpty(publishDate) ? DateTime.Now : Convert.ToDateTime(publishDate);
            var result = Db.Update(m);
            return result ? "OK" : "ERROR";
        }

        private string GetFormValue(string name)
        {
           string s = HttpContext.Current.Request.Form[name].ToString();    
           return s;
        }

        public string Add()
        {
            string publishDate = GetFormValue("PublishDate");
            Archive arc = new Archive()
            {
                Subject = GetFormValue("Subject"),
                Content = GetFormValue("Content"),
                CategoryId = Convert.ToInt32(GetFormValue("CategoryId")),
                PublishDate = string.IsNullOrEmpty(publishDate) ? DateTime.Now : Convert.ToDateTime(publishDate)
            };
            int result = Db.Insert<Archive>(arc);
            return result > 0 ? "OK" : "ERROR";
        }
    }
}