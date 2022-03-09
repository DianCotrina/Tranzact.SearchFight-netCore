using System.Collections.Generic;

namespace Platform.SearchFight.Service.Engine.Model
{
    public class SearchGroupWinner
    {
        public string EngineName { get; set; }
        public IEnumerable<long> Results { get; set; }
    }
}
