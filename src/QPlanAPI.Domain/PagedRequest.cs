using System;
using System.Collections.Generic;
using System.Text;

namespace QPlanAPI.Domain
{
    public class PagedRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
