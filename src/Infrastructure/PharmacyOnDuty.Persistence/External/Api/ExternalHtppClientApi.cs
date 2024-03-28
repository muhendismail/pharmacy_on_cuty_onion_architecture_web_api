using Microsoft.Extensions.Configuration;
using PharmacyOnDuty.Application.Dtos;
using PharmacyOnDuty.Application.External.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PharmacyOnDuty.Persistence.External.Api
{
    public class ExternalHtppClientApi : IExternalApi
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly HttpClient _httpClient;

        public ExternalHtppClientApi(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _clientFactory = clientFactory;
            _httpClient = _clientFactory.CreateClient();

            var httpConfiguration = configuration.GetSection("HttpClient");
            if (httpConfiguration != null)
            {
                var baseAddress = httpConfiguration.GetSection("BaseAddress");
                if (baseAddress != null)
                {
                    _httpClient.BaseAddress = new Uri(baseAddress.Value ?? "");
                }


                var authorization = httpConfiguration.GetSection("Authorization");
                if (authorization != null)
                {
                    _httpClient.DefaultRequestHeaders.Add("Authorization", authorization.Value);
                }
            }
        }
        public async Task<T?> Send<T>(HttpMethod httpMethod, string url) where T : class
        {
            try
            {
                var request = new HttpRequestMessage(httpMethod, url);
                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (content == null) return default;

                    var collectResponseModel = JsonSerializer.Deserialize<CollectResponseModel>(content, new JsonSerializerOptions
                    {
                        IncludeFields = true,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    });

                    if (collectResponseModel == null) return default;

                    return JsonSerializer.Deserialize<T>(collectResponseModel.Result, new JsonSerializerOptions
                    {
                        IncludeFields = true,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    });
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
