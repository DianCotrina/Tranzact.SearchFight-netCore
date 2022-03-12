using Platform.SearchFight.Service.Engine.Model;
using Platform.SearchFight.Service.Engine.Processor.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Platform.SearchFight.Service.Engine.Processor
{
    public class SearchReportProcessor : ISearchReportProcessor
    {
        public List<string> GenerateReportByTopic(IEnumerable<SearchWinner> searchWinners)
        {
            return searchWinners.GroupBy(search => search.Topic).Select(x =>
                $"{x.Key}: {string.Join(" ", x.Select(total => $"{total.EngineName}: {total.Results}"))}").ToList();
        }
    }
}
