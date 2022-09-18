using System.Net.Http;

namespace Frontend.Services.Base
{
    public partial class Client : IClient
    {
        public HttpClient HttpClient
        {
            get{
                return _httpClient;
            }
        }
    }
}
