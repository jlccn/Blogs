using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DapperExtensions;
using Blogs.DAL;
using Blogs.Model;


namespace Blogs
{
    /// <summary>
    /// ArticleBLL 的摘要说明
    /// </summary>
    public class ArticleBLL : System.Web.WebHandler
    {
        IDatabase Db = DBHelper.GetDatabase();
        [ResponseAnnotation(Desc = "取得分页需要的数据源")]
        public object GetPageData(int page, int rows, string key)
        {
            IPredicate pred = null;
            if (!string.IsNullOrEmpty(key))
            {
                key = string.Format("%{0}%", key);
                pred = Predicates.Field<Archive>(p => p.Content, Operator.Like, key);
            }
           return GetData(page, rows, pred);
        }
        public object GetPageDataByCategory(int page, int rows, string key)
        {
            IPredicate pred = null;
            if (!string.IsNullOrEmpty(key))
            {               
                pred = Predicates.Field<Archive>(p => p.CategoryId, Operator.Eq, key);
            }
            return GetData(page, rows, pred);
        }
        public object GetPageDataByMonth(int page, int rows, string key)
        {
            IPredicateGroup predGrp = null;
            if (!string.IsNullOrEmpty(key))
            {
                DateTime dtBegin = DateTime.ParseExact(key + "01", "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                DateTime dtEnd = dtBegin.AddMonths(1);
                var pred1 = Predicates.Field<Archive>(p => p.PublishDate, Operator.Ge, dtBegin);
                var pred2 = Predicates.Field<Archive>(p => p.PublishDate, Operator.Lt, dtEnd);
                predGrp = Predicates.Group(GroupOperator.And, pred1, pred2);
            }            
            return GetData(page, rows, predGrp);
        }

        private object GetData(int page, int rows, IPredicate pred)
        {
            int pageIndex = page - 1;
            int pageSzie = rows;
            var sort = new List<ISort>
                                    {
                                        Predicates.Sort<Archive>(p => p.PublishDate, false)
                                    };

            List<Archive> List = Db.GetPage<Archive>(pred, sort, pageIndex, pageSzie).ToList();
            List.ForEach((p) =>
            {
                p.Content = FormatStr(p.Content, 200);
            });
            int count = Db.Count<Archive>(pred);

            var obj = new { total = count, rows = List };
            return obj;
        }





        public object CountStat()
        {
            int archiveCount = Db.GetList<Archive>().Count();
            int visitCount = Db.GetList<Archive>().Sum(a => a.VisitTotal);
            var obj = new { archiveCount = archiveCount, visitCount = visitCount };
            return obj;
        }
      
        public void UpdateVisitTotal(int id)
        {
            var m = Db.Get<Archive>(id);
            m.VisitTotal = m.VisitTotal + 1;
            Db.Update(m);           
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
                YyyyMm = g.Key.Year.ToString() + g.Key.Month.ToString().PadLeft(2,'0'),
                YM = g.Key.Year.ToString() + "年" + g.Key.Month.ToString() + "月",
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
                Id = p.Id,
                Name = p.Name,
                Count = Db.GetList<Archive>().Count(a => a.CategoryId == p.Id)
            })
            ;
            var obj = new { rows = list };
            return obj;
        }

    }
}