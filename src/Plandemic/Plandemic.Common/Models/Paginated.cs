using System.Collections.Generic;

namespace Plandemic.Common.Models
{
    public class Paginated<T>
    {
        public long Page { get; set; }
        public int Size { get; set; }
        public List<T> Results { get; set; }
        public long TotalCount { get; set; }

        public Paginated()
        {
            Page = 1;
            Size = 1;
            Results = new List<T>();
            TotalCount = 0;
        }

        public Paginated(long page, int size, List<T> results, long totalCount) : this()
        {
            Page = page;
            Size = size;
            Results = results;
            TotalCount = totalCount;
        }
    }
}
