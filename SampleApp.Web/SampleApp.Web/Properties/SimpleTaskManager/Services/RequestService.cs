using SimpleTaskManager.Core.Services;
using SimpleTaskManager.Dtos;
using System.IO;
using System.Net;
using System.Text;

namespace SimpleTaskManager.Services
{
    public class RequestService : IRequestService
    {
        private const string PostMethod = "POST";

        public WebResponseDto SendPostRequest(string endpointUrl, string postData)
        {
            try
            {
                var httpWebRequest = CreateRequest(PostMethod, endpointUrl, postData);
                return SendRequestWithBody(httpWebRequest, postData);
            }

            catch (WebException ex)
            {
                return HandleErrorResponse(ex, endpointUrl, PostMethod);
            }
        }
        private static WebResponseDto SendRequestWithBody(HttpWebRequest request, string requestBody)
        {
            HttpStatusCode httpStatus;
            string responseString = null;
            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(requestBody);
                writer.Close();
                using (WebResponse response = request.GetResponse())
                {
                    httpStatus = (response as HttpWebResponse).StatusCode;

                    using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        responseString = streamReader.ReadToEnd();
                    }
                }
            }

            return new WebResponseDto { HttpStatusCode = httpStatus, Response = responseString };
        }

        private WebResponseDto HandleErrorResponse(WebException exception, string endpointUrl, string method)
        {
            if ((HttpWebResponse)exception.Response == null)
            {
                throw new WebException(string.Format(
                    "An error occured while sending {0} request{1}, {2}", method, endpointUrl, exception.Message));
            }
            using (HttpWebResponse response = (HttpWebResponse)exception.Response)
            {
                if (response.StatusCode != HttpStatusCode.BadRequest && response.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new WebException(string.Format(
                        "An error occured while sending {0} request{1}, {2}", method, endpointUrl, exception.Message));
                }
                else
                {
                    string responseString = null;
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseString = reader.ReadToEnd();
                    }

                    return new WebResponseDto { HttpStatusCode = response.StatusCode, Response = responseString };
                }
            }
        }
        private HttpWebRequest CreateRequest(string httpMethod, string endpointUrl, string patchData)
        {
            return CreateRequestDefault(httpMethod, endpointUrl, patchData);
        }
        private static HttpWebRequest CreateRequestDefault(string httpMethod, string endpointUrl, string requestBody)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endpointUrl);

            request.Method = httpMethod;
            request.ContentType = "application/json";
            request.ContentLength = Encoding.UTF8.GetByteCount(requestBody);
            request.KeepAlive = false;

            return request;
        }
    }
}
