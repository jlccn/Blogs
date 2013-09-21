using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DapperExtensions;


namespace Blogs
{
    /// <summary>
    /// ArticleBLL 的摘要说明
    /// </summary>
    public class ArticleBLL : System.Web.WebHandler
    {
        IDatabase Db = DB.GetDatabase();
        [ResponseAnnotation(Desc = "取得分页需要的数据源")]
        public object GetPageData(int page, int rows)//, string key)
        {
            int pageIndex = page - 1;
            int pageSzie = rows;

            IPredicate pred = null;
            //if (!string.IsNullOrEmpty(key))
            //{
            //    key = string.Format("%{0}%", key);
            //    pred = Predicates.Field<Archive>(p => p.Content, Operator.Like, key);
            //}

            var sort = new List<ISort>
                                    {
                                        Predicates.Sort<Archive>(p => p.Id)
                                    };

            List<Archive> List = Db.GetPage<Archive>(pred, sort, pageIndex, pageSzie).ToList();
            List.ForEach((p) => {
                p.Content = FormatStr(p.Content, 200);
            });
            int count = Db.Count<Archive>(pred);

            var obj = new { total = count, rows = List };
            return obj;
        }

        private string FormatStr(string str, int len)
        {
            str = System.Text.RegularExpressions.Regex.Replace(str, "<[^>]+>", "");
            str = str.Length > len ? str.Substring(0, len) + "......   " : str;
            return str;
        }

        [ResponseAnnotation(Desc = "获取一条数据")]
        public Archive DetailById(int id)
        {
            var result = Db.Get<Archive>(new Archive { Id = id });
            return result;
        }

        [ResponseAnnotation(Desc = "获取按年月分组的文章数")]
        public object GetCountListByMonth()
        {
           var list = Db.GetList<Archive>().GroupBy(p => new {
                Year = p.PublishDate.Year,
                Month = p.PublishDate.Month
            }).Select( g=> new 
            {
                YM = g.Key.Year + "年" + g.Key.Month + "月",
                Count = g.Count()
            }).OrderByDescending(g=>g.YM.Substring(0,4))
            .ThenByDescending(g => g.YM.Substring(5, g.YM.Length - 6));
           var obj = new { rows = list };
           return obj;
        }

        [ResponseAnnotation(Desc = "获取按文章类型分组的文章数")]
        public object GetCountListByCategory()
        {
            var list = Db.GetList<Category>().Select(p => new
            {
                Name = p.Name,
                Count = Db.GetList<Archive>().Count(a => a.CategoryId == p.Id)
            })
            ;
            var obj = new { rows = list };
            return obj;
        }

    }
}