using System.Collections.Generic;
using System.Threading.Tasks;
using Platform.SearchFight.Service.Engine.Model;

namespace Platform.SearchFight.Service.Engine.Processor.Interfaces
{
    public interface ISearchTopicProcessor
    {
        Task<List<SearchWinner>> Invoke(List<string> searchTopics,
            IList<SearchWinner> searchResults);

        string GetTopicWinner(IEnumerable<SearchWinner> searchWinners);
    }
}