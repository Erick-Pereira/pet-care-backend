using Commons.Responses;
using DAL.Interfaces;

namespace DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IAddressDAL AddressRepository { get; }
        IBreedDAL BreedRepository { get; }
        ICityDAL CityRepository { get; }
        INeighborhoodDAL NeighborhoodRepository { get; }
        ISpecieDAL SpecieRepository { get; }
        IStateDAL StateRepository { get; }
        IUserDAL UserRepository { get; }
        IPetDAL PetRepository { get; }

        Task<Response> Commit();

        Task<Response> CommitForUser();
    }
}