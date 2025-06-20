using Commons.Interfaces;
using Commons.Responses;
using Entities;

namespace BLL.Interfaces
{
    public interface IStateService : ICRUD<State>
    {
        Task<SingleResponse<State>> FindByAbbreviation(string name);

        Task<SingleResponse<State>> ToggleActive(Guid id);
    }
}