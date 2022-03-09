using Platform.SearchFight.Service.Engine.Model;
using System.Collections.Generic;

namespace Platform.SearchFight.Service.Engine.Converter
{
    public interface IGroupListConverter
    {
        IEnumerable<string> ConvertGroupList(IEnumerable<SearchWinner> reportsGroupedByEngineWinners);
    }
}