using Microsoft.AspNetCore.Mvc.Testing;
using NasaApi.Application.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NasaApiApp.ApiTest
{
    public class NasaLibraryApiTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        public NasaLibraryApiTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        [Fact]
        public async Task Test_Get_All()
        {

            var q = "apollo 10";
            var media_type = "image";
            var client = _factory.CreateClient();
            var response = await client.GetAsync($"api/NasaLibrary/data?FreeText={q}&MediaType={media_type}");

            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                var result =await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<NasaResponse>(result);
            }

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }
    }
}
