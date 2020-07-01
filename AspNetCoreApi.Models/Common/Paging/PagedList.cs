using System;
using System.Collections.Generic;

namespace AspNetCoreApi.Models.Common.Paging
{
    public class PagedList<T>
    {
        public List<T> Items { get; set; }
        public Pagination Pagination { get; set; }

        public PagedList()
        { }

        public PagedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            Pagination = new Pagination
            {
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };

            Items = new List<T>();
            Items.AddRange(items);
        }
    }
}