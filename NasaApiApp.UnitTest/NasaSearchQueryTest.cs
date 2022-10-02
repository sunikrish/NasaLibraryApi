using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using NasaApi.Application.Query;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NasaApiApp.UnitTest
{
    public class NasaSearchQueryTest
    {
        public Mock<ILogger<GetNasaSearchQueryHandler>> logger = new Mock<ILogger<GetNasaSearchQueryHandler>>();
        public Mock<IHttpClientFactory> httpClientFactory = new Mock<IHttpClientFactory>();
        public Mock<IConfiguration> configuration = new Mock<IConfiguration>();
        [Fact]
        public async void Should_Return_Success_When_SearchBy_RandomText()
        {
            //Assign
            #region mock httpclient
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
               .Protected()
               // Setup the PROTECTED method to mock
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               // prepare the expected response of the mocked http call
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent("{\"collection\":{\"href\":\"http://images-api.nasa.gov/search?q=apollo%2011&description=moon%landing&media_type=image&year_start=2019&year_end=2020&page=3\",\"items\":[{\"href\":\"https://images-assets.nasa.gov/image/jsc2022e045837/collection.json\",\"data\":[{\"center\":\"JSC\",\"date_created\":\"2019-07-19T00:00:00Z\",\"description\":\"VicePresidentMike.\",\"media_type\":\"image\",\"nasa_id\":\"jsc2022e045837\",\"photographer\":\"NASA/RadislavSinyak\",\"title\":\"VicePresidentUnveilsNASASpacecraftforArtemis1LunarMissi\"}],\"links\":[{\"href\":\"https://images-assets.nasa.gov/image/jsc2022e045837/jsc2022e045837~thumb.jpg\",\"rel\":\"preview\",\"render\":\"image\"}]}],\"links\":[{\"href\":\"http://images-api.nasa.gov/search?q=apollo+11&description=moon%25landing&media_type=image&year_start=2019&year_end=2020&page=2\"}]}}")
               }).Verifiable();

            // use real http client with mocked handler here
            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            #endregion

            // Inject the handler or client into your application code

            httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(httpClient);
            configuration.Setup(x => x.GetSection("NasaApi:Url").Value).Returns("http://test.com/");
            GetNasaSearchQuery request = new GetNasaSearchQuery
            {
                FreeText = "apollo 11",
                MediaType = "image"
            };

            //Act

            GetNasaSearchQueryHandler handler = new GetNasaSearchQueryHandler(httpClientFactory.Object, configuration.Object, logger.Object);
            var response = await handler.Handle(request, new System.Threading.CancellationToken());

            //Assert
            Assert.NotNull(response.Collection);
        }
    }
}
