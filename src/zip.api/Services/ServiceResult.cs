using System.Net;

namespace zip.api.Services
{
    public class ServiceResult<T>
    {
        public T Model { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public ServiceResult(T model, HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
            Model = model;
        }

    }
}
