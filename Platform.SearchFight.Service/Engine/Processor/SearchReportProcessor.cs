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

        public IEnumerable<SearchWinner> GenerateReportByEngineWinner(IEnumerable<SearchWinner> searchWinners)
        {
            var groupListByEngineName = searchWinners.GroupBy(x => x.EngineName, x => x.Results, (engineName, results) =>
                new SearchGroupWinner
                {
                    EngineName = engineName,
                    Results = results
                });

            var searchWinnerList = groupListByEngineName
                .Select(o => new SearchWinner
                {
                    EngineName = o.EngineName,
                    Results = o.Results.Max(),
                    Topic = GetTopicWinner(searchWinners, o.EngineName, o.Results.Max())
                });

            return searchWinnerList;
        }

        private string GetTopicWinner(IEnumerable<SearchWinner> groupListByEngineName, string engine, long max)
        {
            var topicWinner = "";
            foreach (var groupList in groupListByEngineName)
            {
                if (groupList.EngineName != engine || groupList.Results != max) continue;
                topicWinner = groupList.Topic;
            }
            return topicWinner;
        }
    }
}
