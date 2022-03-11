using System;
using System.Net;

namespace Common.StandardInfrastructure
{
    public class ResultDto
    {
        public object Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }

        public ResultDto()
        {

        }
        public ResultDto(object data = null, HttpStatusCode statusCode = HttpStatusCode.BadRequest, string message = null, Exception exception = null)
        {
            Data = data;
            StatusCode = statusCode;
            Message = message;
            Exception = exception;
        }
    }
}
