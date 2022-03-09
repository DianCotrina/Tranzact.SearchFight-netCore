using Platform.SearchFight.Service.Engine.Model;
using System.Threading.Tasks;

namespace Platform.SearchFight.Service.Engine.Interfaces
{
    public interface ISearchEngine
    {
        Task<SearchWinner> GetSearchResultCount(string topic);
    }
}
