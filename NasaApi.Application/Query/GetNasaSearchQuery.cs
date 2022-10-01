using System.Threading.Tasks;
using Newtonsoft.Json;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using MediatR;
using System.Threading;
using System.Net.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace NasaApi.Application.Query
{
    public class GetNasaSearchQuery : IRequest<NasaResponse>
    {
        [StringLength(150)]
        public string FreeText { get; set; }

        [RegularExpression(@"\d{4}", ErrorMessage = "start date must be year.")]
        public string StartYear { get; set; }

        [RegularExpression(@"\d{4}", ErrorMessage = "end date must be year.")]
        public string EndYear { get; set; }

        [RegularExpression("image|audio", ErrorMessage = "media type should be either image or audio")]
        public string MediaType { get; set; }

    }

    public class GetNasaSearchQueryHandler : IRequestHandler<GetNasaSearchQuery, NasaResponse>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<GetNasaSearchQueryHandler> _logger;

        public GetNasaSearchQueryHandler(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<GetNasaSearchQueryHandler> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<NasaResponse> Handle(GetNasaSearchQuery request, CancellationToken cancellationToken)
        {
            HttpClient client = _httpClientFactory.CreateClient();
            NasaResponse response = null;
            string baseurl = _configuration.GetSection("NasaApi:Url").Value;

            var queryParams = new Dictionary<string, string>();
            if (request.FreeText != null)
                queryParams.Add("q", request.FreeText);

            if (request.StartYear != null)
                queryParams.Add("year_start", request.StartYear);

            if (request.EndYear != null)
                queryParams.Add("year_end", request.EndYear);

            if (request.MediaType != null)
                queryParams.Add("media_type", request.MediaType);
            string url = QueryHelpers.AddQueryString($"{baseurl}", queryParams);

            HttpResponseMessage responseMessage = await client.GetAsync(url);
            if (responseMessage.IsSuccessStatusCode)
            {
                var result = responseMessage.Content.ReadAsStringAsync();
                response = JsonConvert.DeserializeObject<NasaResponse>(result.Result);
            }

            return await Task.FromResult(response);
        }
    }
}
