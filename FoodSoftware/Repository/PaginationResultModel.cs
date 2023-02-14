using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class PaginationResultModel<T>
    {
        public List<T> Data { get; set; }
        public int? Page { get; set; }
        public int? Pages { get; set; }
        public int? Limit { get; set; }
        public int? Total { get; set; }
    }
}
