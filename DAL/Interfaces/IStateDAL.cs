using Commons.Interfaces;
using Commons.Responses;
using Entities;

namespace DAL.Interfaces
{
    public interface IStateDAL : ICRUD<State>
    {
        Task<SingleResponse<State>> FindByAbbreviation(string name);
    }
}