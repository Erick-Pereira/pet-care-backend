using Commons.Interfaces;
using Commons.Responses;
using Entities;

namespace BLL.Interfaces
{
    public interface IStateService : ICRUD<State>
    {
        Task<SingleResponse<State>> FindByAbreviation(string name);

        Task<SingleResponse<State>> ToggleActive(Guid id);
    }
}