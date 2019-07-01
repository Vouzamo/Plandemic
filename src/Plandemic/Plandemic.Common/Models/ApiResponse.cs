using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Net;

namespace Plandemic.Common.Models
{
    public class ApiResponse
    {
        public HttpStatusCode Status { get; set; }
        public Dictionary<string, StringValues> Errors { get; set; }
        public string Title { get; set; }

        public ApiResponse()
        {
            Status = HttpStatusCode.OK;
            Errors = new Dictionary<string, StringValues>();
        }

        public ApiResponse(HttpStatusCode statusCode, string title = null) : this()
        {
            Status = statusCode;
            Title = title;
        }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T Source { get; set; }

        public ApiResponse() : base()
        {
            Source = default;
        }

        public ApiResponse(HttpStatusCode statusCode, T source = default, string title = null) : this()
        {
            Status = statusCode;
            Source = source;
            Title = title;
        }
    }
}
