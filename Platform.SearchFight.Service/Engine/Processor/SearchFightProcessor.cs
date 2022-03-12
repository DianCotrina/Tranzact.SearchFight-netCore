using Platform.SearchFight.Service.Engine.Converter;
using Platform.SearchFight.Service.Engine.Model;
using Platform.SearchFight.Service.Engine.Processor.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Platform.SearchFight.Service.Engine.Processor
{
    public class SearchFightProcessor
    {
        private readonly ISearchTopicProcessor _searchTopicProcessor;
        private readonly IGroupListConverter _groupListConverter;
        private readonly ISearchReportProcessor _searchReportProcessor;
        private readonly IWinnerProcessor _winnerProcessor;

        public List<string> Reports { get; }

        public SearchFightProcessor(ISearchTopicProcessor searchTopicProcessor, IGroupListConverter groupListConverter, ISearchReportProcessor searchReportProcessor, IWinnerProcessor winnerProcessor)
        {
            _searchTopicProcessor = searchTopicProcessor;
            _groupListConverter = groupListConverter;
            _searchReportProcessor = searchReportProcessor;
            _winnerProcessor = winnerProcessor;

            Reports = new List<string>();
        }

        public async Task CallSearchFightService(List<string> searchTopics)
        {
            var serviceResponse = await _searchTopicProcessor.Invoke(searchTopics);

            var reportsGroupedByTopic = _searchReportProcessor.GenerateReportByTopic(serviceResponse);

            var reportsGroupedByEngineWinners = _winnerProcessor.GenerateReportByEngineWinner(serviceResponse);

            Reports.AddRange(reportsGroupedByTopic);
            Reports.AddRange(_groupListConverter.ConvertGroupList(reportsGroupedByEngineWinners));

            var totalWinner = _searchTopicProcessor.GetTopicWinner(reportsGroupedByEngineWinners);
            Reports.Add($"Total winner: {totalWinner}");
        }

        
    }
}
