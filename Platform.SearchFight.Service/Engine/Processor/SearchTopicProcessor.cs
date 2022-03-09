using Platform.SearchFight.Service.Engine.Interfaces;
using Platform.SearchFight.Service.Engine.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Platform.SearchFight.Service.Engine.Processor.Interfaces;

namespace Platform.SearchFight.Service.Engine.Processor
{
    public class SearchTopicProcessor : ISearchTopicProcessor
    {
        private readonly ISearchEngine[] _searchEngines;

        public SearchTopicProcessor(ISearchEngine[] searchEngines)
        {
            _searchEngines = searchEngines;
        }

        public async Task<List<SearchWinner>> Invoke(List<string> searchTopics,
            IList<SearchWinner> searchResults)
        {
            foreach (var searchEngine in _searchEngines)
            {
                foreach (string topic in searchTopics)
                {
                    var result = await searchEngine.GetSearchResultCount(topic);

                    searchResults?.Add(result);
                }
            }

            return searchResults?.ToList();
        }

        public string GetTopicWinner(IEnumerable<SearchWinner> searchWinners)
        {
            List<SearchWinner> enumerable = searchWinners.ToList();
            long winnerNumber = enumerable.Select(x => x.Results).Max();
            string winnerTopic = null;

            foreach (var result in enumerable)
            {
                if (result.Results == winnerNumber)
                    winnerTopic = result.Topic;
            }

            return winnerTopic;
        }
    }
}
