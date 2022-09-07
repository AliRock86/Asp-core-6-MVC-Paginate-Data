using Microsoft.EntityFrameworkCore;

namespace Pagingtion.Models
{
    public class PaginateList<T>:List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get;  set; }

        public PaginateList(List<T> items ,int count ,int pageIndex , int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count/(double)pageSize);
            this.AddRange(items);
        }

        public bool PreviousPage
        {
            get { return PageIndex > 1; }
        }

        public bool NextPage
        {
            get { return PageIndex < TotalPages; }
        }

        public static async Task<PaginateList<T>> CreateAsync(IQueryable<T> source , int pageIndex , int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex -1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginateList<T>(items ,count ,pageIndex ,pageSize);
        }
    }
}
