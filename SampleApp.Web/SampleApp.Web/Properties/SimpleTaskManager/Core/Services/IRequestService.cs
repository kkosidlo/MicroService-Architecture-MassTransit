using SimpleTaskManager.Dtos;

namespace SimpleTaskManager.Core.Services
{
    public interface IRequestService
    {
        WebResponseDto SendPostRequest(string endpointUrl, string postData);
    }
}
