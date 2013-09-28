using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blogs.DAL;
using Blogs.Common;
using LinqKit;
using System.Linq.Expressions;

namespace Blogs
{
    /// <summary>
    /// ArticleBLL 的摘要说明
    /// </summary>
    public class ArticleBLL : System.Web.WebHandler
    {
        [ResponseAnnotation(Desc = "取得分页需要的数据源")]
        public object GetPageData(int page, int rows, string key)
        {
            var predicate = PredicateBuilder.True<Archive>();
            return GetPageData(page, rows, predicate);
        }

        private object GetPageData(int page, int rows, Expression<Func<Archive, bool>> predicate)
        {
            using (SysEntities db = new SysEntities())
            {
                int totalCount = 0;
                var list = db.Archive.GetPager(predicate, "PublishDate desc,Id desc", page, rows, out totalCount)
                        .ToList()
                        .Select(c => new
                        {
                            Id = c.Id,
                            Subject = c.Subject,
                            PublishDate = c.PublishDate,
                            Content = FormatStr(c.Content, 200),
                            VisitTotal = c.VisitTotal
                        });
                var obj = new { total = totalCount, rows = list };
                return obj;
            }
        }

        public object GetPageDataByCategory(int page, int rows, string key)
        {
            long categoryId = Convert.ToInt64(key);
            var predicate = PredicateBuilder.True<Archive>();
            predicate = predicate.And(p => p.CategoryId == categoryId);
            return GetPageData(page, rows, predicate);
        }
        public object GetPageDataByMonth(int page, int rows, string key)
        {
            DateTime dtBegin = DateTime.ParseExact(key + "01", "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            DateTime dtEnd = dtBegin.AddMonths(1);
            var predicate = PredicateBuilder.True<Archive>();           
            //LINQ to Entities 不识别方法“System.String ToString(System.String)”，因此该方法无法转换为存储表达式。
            //predicate = predicate.And(p => Convert.ToDateTime(p.PublishDate).ToString("yyyyMM") == key);
            predicate = predicate.And(p => p.PublishDate >= dtBegin);
            predicate = predicate.And(p => p.PublishDate < dtEnd);
            return GetPageData(page, rows, predicate);
        }


        public object CountStat()
        {
            using (SysEntities db = new SysEntities())
            {
                int archiveCount = db.Archive.Count();
                long? visitCount = db.Archive.Sum(a => a.VisitTotal);
                var obj = new { archiveCount = archiveCount, visitCount = visitCount };
                return obj;
            }
        }

        public void UpdateVisitTotal(int id)
        {
            using (SysEntities db = new SysEntities())
            {
                var m = db.Archive.Where(a => a.Id == id).FirstOrDefault();
                m.VisitTotal = m.VisitTotal + 1;
                //db.Archive.Attach(m);
                db.ObjectStateManager.ChangeObjectState(m, System.Data.EntityState.Modified);
                db.SaveChanges();
            }
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
            using (SysEntities db = new SysEntities())
            {
                var result = db.Archive.Where(a => a.Id == id).FirstOrDefault();
                return result;
            }
        }

        [ResponseAnnotation(Desc = "获取按年月分组的文章数")]
        public object GetCountListByMonth()
        {
            using (SysEntities db = new SysEntities())
            {
                var list = db.Archive.GroupBy(a => new
                {
                    Year = a.PublishDate.Value.Year,
                    Month = a.PublishDate.Value.Month
                })
                .ToList()
                .Select(g => new
                {
                    YyyyMm = g.Key.Year.ToString() + g.Key.Month.ToString().PadLeft(2, '0'),
                    YM = g.Key.Year.ToString() + "年" + g.Key.Month.ToString() + "月",
                    Count = g.Count()
                })
                .OrderByDescending(g => g.YM.Substring(0, 4))
                .ThenByDescending(g => g.YM.Substring(5, g.YM.Length - 6));

                var obj = new { rows = list };
                return obj;
            }
        }

        [ResponseAnnotation(Desc = "获取按文章类型分组的文章数")]
        public object GetCountListByCategory()
        {
            using (SysEntities db = new SysEntities())
            {
                var list = db.Category.Select(t => new
                   {
                       Id = t.Id,
                       Name = t.Name,
                       Count = db.Archive.Count(a => a.CategoryId == t.Id)
                   }).ToList();
                var obj = new { rows = list };
                return obj;
            }
        }

    }
}