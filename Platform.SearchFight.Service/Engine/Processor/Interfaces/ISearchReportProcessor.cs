using System.Collections.Generic;
using Platform.SearchFight.Service.Engine.Model;

namespace Platform.SearchFight.Service.Engine.Processor.Interfaces
{
    public interface ISearchReportProcessor
    {
        List<string> GenerateReportByTopic(IEnumerable<SearchWinner> searchWinners);
        IEnumerable<SearchWinner> GenerateReportByEngineWinner(IEnumerable<SearchWinner> searchWinners);
        
    }
}