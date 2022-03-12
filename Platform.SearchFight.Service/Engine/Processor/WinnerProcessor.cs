using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Platform.SearchFight.Service.Engine.Model;
using Platform.SearchFight.Service.Engine.Processor.Interfaces;

namespace Platform.SearchFight.Service.Engine.Processor
{
    public class WinnerProcessor : IWinnerProcessor
    {
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
                    Topic = GetWinnerPerEngineName(searchWinners, o.EngineName, o.Results.Max())
                });

            return searchWinnerList;
        }

        private static string GetWinnerPerEngineName(IEnumerable<SearchWinner> groupListByEngineName, string engine, long max)
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
