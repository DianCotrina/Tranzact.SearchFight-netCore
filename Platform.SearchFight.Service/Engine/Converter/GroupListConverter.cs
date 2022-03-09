using Platform.SearchFight.Service.Engine.Model;
using System.Collections.Generic;
using System.Text;

namespace Platform.SearchFight.Service.Engine.Converter
{
    public class GroupListConverter : IGroupListConverter
    {
        public IEnumerable<string> ConvertGroupList(IEnumerable<SearchWinner> reportsGroupedByEngineWinners)
        {
            List<string> convertedList = new List<string>();

            foreach (SearchWinner searchWinner in reportsGroupedByEngineWinners)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append($"{searchWinner.EngineName} winner : {searchWinner.Topic}");
                convertedList.Add(builder.ToString());
            }

            return convertedList;
        }
    }
}
