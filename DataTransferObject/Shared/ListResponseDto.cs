using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObject.Shared
{
    public class ListResponseDto<TList>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public List<TList> List { get; set; }
    }
}
