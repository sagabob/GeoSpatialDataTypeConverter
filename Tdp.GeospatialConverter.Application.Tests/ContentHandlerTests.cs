using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Tdp.GeospatialConverter.Application.Handlers;
using Xunit;

namespace Tdp.GeospatialConverter.Application.Tests
{
    public class ContentHandlerTests
    {
        private ContentHandler _contentHandler;

        [Theory]
        [InlineData("OK", HttpStatusCode.Accepted)]
        [InlineData("OK", HttpStatusCode.BadRequest)]
        public async Task ShouldGetExpectedContentFromUrlAsync(string content, HttpStatusCode code)
        {
            // arrange
            var url = "http://tdp-itplayground.info/assets/kmlsamples/kmlcontent.xml";

            var response = new HttpResponseMessage(code)
            {
                Content = new StringContent(content)
            };

            var fakeHttpMessageHandler = new FakeHttpMessageHandler(response);

            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);

            _contentHandler = new ContentHandler(fakeHttpClient);


            // act
            var expectedResponse = await _contentHandler.GetContentFromUrlAsync(url);


            // assert
            response.Content.Should().Be(expectedResponse.Content);
            response.StatusCode.Should().Be(expectedResponse.StatusCode);
        }
    }
}