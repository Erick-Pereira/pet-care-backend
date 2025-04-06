using Commons.Responses;

namespace Commons.Interfaces
{
    public interface ICRUD<T>
    {
        Task<Response> Insert(T Item);

        Task<SingleResponse<T>> Get(Guid id);

        Task<DataResponse<T>> Get(int skip, int take);

        Task<Response> Update(T Item);

        Task<Response> Delete(Guid id);
    }
}