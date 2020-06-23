using System.Net.Http;
using System.Threading.Tasks;

namespace Tdp.GeospatialConverter.Application.Handlers
{
    public class ContentHandler
    {
        private readonly HttpClient _httpClient;

        public ContentHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<HttpResponseMessage> GetContentFromUrlAsync(string requestUrl)
        {
            return _httpClient.GetAsync(requestUrl);
        }
    }
}