using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Platform.SearchFight.Service.Engine.Config;
using Platform.SearchFight.Service.Engine.Interfaces;
using Platform.SearchFight.Service.Engine.Model;
using Platform.SearchFight.Service.Engine.Model.Response;

namespace Platform.SearchFight.Service.Engine
{
    public class BingSearchEngine : ISearchEngine
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly SearchBingConfig _configuration;
        public BingSearchEngine(IOptions<SearchBingConfig> configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration.Value;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<SearchWinner> GetSearchResultCount(string topic)
        {
            try
            {
                if (string.IsNullOrEmpty(topic))
                    throw new ArgumentNullException(nameof(topic), "Topic is invalid. Please select a valid topic");

                var client = _httpClientFactory.CreateClient();

                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _configuration.ApiKey);

                string baseUrl = _configuration.BaseUrl;

                var response = await client.GetAsync($"{baseUrl}{topic}&count=1");

                string result = await response.Content.ReadAsStringAsync();

                var searchInformation = JsonConvert.DeserializeObject<BingResponse>(result);

                if (searchInformation != null)
                {
                    return new SearchWinner()
                    {
                        EngineName = _configuration.EngineName, 
                        Topic = topic,
                        Results = long.Parse(searchInformation.WebPages.TotalEstimatedMatches)
                    };
                }

                throw new ArgumentException("The current process has failed. No data encountered");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
