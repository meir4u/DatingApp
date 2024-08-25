using DatingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace DatingApp.Application.Pagination
{
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            PageSize = pageSize;
            TotalCount = count;
            AddRange(items);
        }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> sourse, int pageNumber, int pageSize)
        {
            //var count = await sourse.CountAsync();
            //var items = await sourse.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            var query = sourse
                            .Select(x => new { TotalCount = sourse.Count(), Item = x })
                            .Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize);

            var result = await query.ToListAsync();
            var totalCount = result.FirstOrDefault()?.TotalCount ?? 0;
            var items = result.Select(x => x.Item).ToList();

            return new PagedList<T>(items, totalCount, pageNumber, pageSize);
        }
    }
}