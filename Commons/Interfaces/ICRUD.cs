using Commons.Responses;

namespace Commons.Interfaces
{
    public interface ICRUD<T>
    {
        Task<Response> Insert(T item);

        Task<SingleResponse<T>> Get(Guid id);

        Task<DataResponse<T>> Get(int skip, int take, string? filter);

        Task<Response> Update(T item);

        Task<Response> Delete(Guid id);
    }
}