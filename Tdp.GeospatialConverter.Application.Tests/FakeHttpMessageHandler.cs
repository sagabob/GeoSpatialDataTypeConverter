using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Tdp.GeospatialConverter.Application.Tests
{
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage _fakeResponse;

        public FakeHttpMessageHandler(HttpResponseMessage fakeResponse)
        {
            _fakeResponse = fakeResponse;
        }

        public virtual HttpResponseMessage Send(HttpRequestMessage request)
        {
            // Configure this method however you wish for your testing needs.
            return _fakeResponse;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(Send(request));
        }
    }
}