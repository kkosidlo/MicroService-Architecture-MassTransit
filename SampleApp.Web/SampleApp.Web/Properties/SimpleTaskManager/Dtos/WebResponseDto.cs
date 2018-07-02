using System.Net;

namespace SimpleTaskManager.Dtos
{
    public class WebResponseDto
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public string Response { get; set; }
    }
}
