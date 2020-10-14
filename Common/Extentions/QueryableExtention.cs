using DataTransferObject.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extentions
{
    public static class QueryableExtension
    {
        public static IQueryable<T> GetPaged<T>(this IQueryable<T> query, int page, int pageSize, string title) where T : class
        {
            var result = new ListResponseDto<T> { Page = page, PageSize = pageSize, Count = query.Count() };
            var pageCount = (double)result.Count / pageSize;
            var pages = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            return query.Skip(skip).Take(pageSize);


        }
    }
}
