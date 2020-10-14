using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObject.Shared
{
    public class ListRequestDto<TRequest>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public TRequest Search { get; set; }

    }
}
