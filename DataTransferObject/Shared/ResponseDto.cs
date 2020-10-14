using System;
using System.Collections.Generic;
using System.Text;

namespace DataTransferObject.Shared
{
    public class ResponseDto<TResponse>
    {
        public bool Successful { get; set; }
        public string Title { get; set; }
        public TResponse  Response { get; set; }

    }
    public class ResponseDto
    {
        public bool Successful { get; set; }
        public string Title { get; set; }

    }
}
