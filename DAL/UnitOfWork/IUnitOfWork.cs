using Commons.Responses;
using DAL.Interfaces;

namespace DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IAddressDAL AddressRepository { get; }
        IBreedDAL BreedRepository { get; }
        ICityDAL CityRepository { get; }
        IDiagnosisDAL DiagnosisRepository { get; }
        IDocumentAttachmentDAL DocumentAttachmentRepository { get; }
        IDocumentDAL DocumentRepository { get; }
        IExamDAL ExamRepository { get; }
        IMedicalAttachmentDAL MedicalAttachmentRepository { get; }
        IMedicalEventDAL MedicalEventRepository { get; }
        IMedicationDAL MedicationRepository { get; }
        INeighborhoodDAL NeighborhoodRepository { get; }
        IPetDAL PetRepository { get; }
        IPetPhotoDAL PetPhotoRepository { get; }
        ISpecieDAL SpecieRepository { get; }
        IStateDAL StateRepository { get; }
        IUserDAL UserRepository { get; }
        IVaccineDAL VaccineRepository { get; }

        Task<Response> Commit();

        Task<Response> CommitForUser();
    }
}