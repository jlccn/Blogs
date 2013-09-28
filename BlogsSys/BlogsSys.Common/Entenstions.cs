using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using LinqKit;

namespace Blogs.Common
{
    public static class Entenstions
    {
        /// <summary>
        /// 分页
        /// </summary> 
        //public static IQueryable<T> GetPager<T>(this IQueryable<T> List, Expression<Func<T, bool>> predicate, Func<T, DateTime> FunOrder, 
        //    int PageIndex, int PageSize, out int totalCount)
        //{
        //    var rance = List.AsExpandable().Where(predicate);          
        //    totalCount = rance.Count();
        //    return rance.OrderByDescending(FunOrder).Select(t => t).Skip(
        //        (PageIndex - 1) * PageSize).Take(PageSize).AsQueryable();
        //}
        /// <summary>
        /// 分页
        /// </summary> 
        public static IQueryable<T> GetPager<T>(this IQueryable<T> List, Expression<Func<T, bool>> predicate, string orderFields,
            int PageIndex, int PageSize, out int totalCount)
        {
            var rance = List.AsExpandable().Where(predicate);
            totalCount = rance.Count();
            return rance.OrderBy(orderFields).Select(t => t).Skip(
                (PageIndex - 1) * PageSize).Take(PageSize).AsQueryable();
        }
    }
}
