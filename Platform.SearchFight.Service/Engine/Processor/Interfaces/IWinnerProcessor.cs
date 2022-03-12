using System;
using System.Collections.Generic;
using System.Text;
using Platform.SearchFight.Service.Engine.Model;

namespace Platform.SearchFight.Service.Engine.Processor.Interfaces
{
    public interface IWinnerProcessor
    {
        IEnumerable<SearchWinner> GenerateReportByEngineWinner(IEnumerable<SearchWinner> searchWinners);
    }
}
