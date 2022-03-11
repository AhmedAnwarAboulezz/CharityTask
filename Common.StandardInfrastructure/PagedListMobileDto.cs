using System.Collections.Generic;

namespace Common.StandardInfrastructure
{
    public class PagedListMobileDto<T> where T : class
    {
        public IEnumerable<T> List { get; set; }
        public int Count { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int PagesCount { get; set; }
    }
}
