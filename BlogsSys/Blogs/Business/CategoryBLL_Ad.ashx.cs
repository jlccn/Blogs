using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DapperExtensions;

namespace Blogs.Business
{
    /// <summary>
    /// CategoryBLL_Ad 的摘要说明
    /// </summary>
    public class CategoryBLL_Ad : System.Web.WebHandler
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
                pred = Predicates.Field<Category>(p => p.Name, Operator.Like, key);
            }
            var sort = new List<ISort>
                                    {
                                        Predicates.Sort<Category>(p => p.Id)
                                    };

            var List = Db.GetPage<Category>(pred, sort, pageIndex, pageSzie).ToList();
            int count = Db.Count<Category>(pred);
            var obj = new { total = count, rows = List };
            return obj;
        }

        public object GetList()
        {
            var result = Db.GetList<Category>();
            return result;
        }

        public object Get(int id)
        {
            var result = Db.Get<Category>(new Category { Id = id });
            return result;
        }

        [ResponseAnnotation(Desc = "删除一条数据")]
        public string Delete(int id)
        {
            var result = Db.Delete<Category>(new Category { Id = id });
            return result ? "1" : "0";
        }
        [ResponseAnnotation(Desc = "更新一条数据")]
        public string Update(int id)
        {
            var m = Db.Get<Category>(id);
            m.Name = GetFormValue("Name");          
            var result = Db.Update(m);
            return result ? "1" : "0";
        }
        [ResponseAnnotation(Desc = "添加一条数据")]
        public string Add()
        {
            Category arc = new Category()
            {
                Name = GetFormValue("Name")
            };
            int result = Db.Insert<Category>(arc);
            return result > 0 ? "1" : "0";
        }
        private string GetFormValue(string name)
        {
            return HttpContext.Current.Request.Form[name].ToString();
        }
    }
}