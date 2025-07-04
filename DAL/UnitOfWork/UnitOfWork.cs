﻿using BLL.ErrorHandling;
using Commons.Responses;
using DAL.Impl;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;

namespace DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DataBaseDbContext _dbContext;
        private readonly ILoggerFactory _loggerFactory;
        private IAddressDAL? _addressRepository;
        private IBreedDAL? _breedRepository;
        private ICityDAL? _cityRepository;
        private IDiagnosisDAL? _diagnosisRepository;
        private IDocumentAttachmentDAL? _documentAttachmentRepository;
        private IDocumentDAL? _documentRepository;
        private IExamDAL? _examRepository;
        private IMedicalAttachmentDAL? _medicalAttachmentRepository;
        private IMedicalEventDAL? _medicalEventRepository;
        private IMedicationDAL? _medicationRepository;
        private INeighborhoodDAL? _neighborhoodRepository;
        private IPetPhotoDAL? _petPhotoRepository;
        private IPetDAL? _petRepository;
        private ISpecieDAL? _specieRepository;
        private IStateDAL? _stateRepository;
        private IUserDAL? _userRepository;
        private IVaccineDAL? _vaccineRepository;
        private bool _disposed = false;

        public UnitOfWork(DataBaseDbContext context, ILoggerFactory loggerFactory)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        public IAddressDAL AddressRepository
        {
            get
            {
                if (_addressRepository == null)
                {
                    var logger = _loggerFactory.CreateLogger<AddressDALImpl>();
                    _addressRepository = new AddressDALImpl(_dbContext, logger);
                }
                return _addressRepository;
            }
        }

        public IBreedDAL BreedRepository
        {
            get
            {
                if (_breedRepository == null)
                {
                    var logger = _loggerFactory.CreateLogger<BreedDALImpl>();
                    _breedRepository = new BreedDALImpl(_dbContext, logger);
                }
                return _breedRepository;
            }
        }

        public ICityDAL CityRepository
        {
            get
            {
                if (_cityRepository == null)
                {
                    var logger = _loggerFactory.CreateLogger<CityDALImpl>();
                    _cityRepository = new CityDALImpl(_dbContext, logger);
                }
                return _cityRepository;
            }
        }

        public IDiagnosisDAL DiagnosisRepository
        {
            get
            {
                if (_diagnosisRepository == null)
                {
                    var logger = _loggerFactory.CreateLogger<DiagnosisDALImpl>();
                    _diagnosisRepository = new DiagnosisDALImpl(_dbContext, logger);
                }
                return _diagnosisRepository;
            }
        }

        public IDocumentAttachmentDAL DocumentAttachmentRepository
        {
            get
            {
                if (_documentAttachmentRepository == null)
                {
                    var logger = _loggerFactory.CreateLogger<DocumentAttachmentDALImpl>();
                    _documentAttachmentRepository = new DocumentAttachmentDALImpl(_dbContext, logger);
                }
                return _documentAttachmentRepository;
            }
        }

        public IDocumentDAL DocumentRepository
        {
            get
            {
                if (_documentRepository == null)
                {
                    var logger = _loggerFactory.CreateLogger<DocumentDALImpl>();
                    _documentRepository = new DocumentDALImpl(_dbContext, logger);
                }
                return _documentRepository;
            }
        }

        public IExamDAL ExamRepository
        {
            get
            {
                if (_examRepository == null)
                {
                    var logger = _loggerFactory.CreateLogger<ExamDALImpl>();
                    _examRepository = new ExamDALImpl(_dbContext, logger);
                }
                return _examRepository;
            }
        }

        public IMedicalAttachmentDAL MedicalAttachmentRepository
        {
            get
            {
                if (_medicalAttachmentRepository == null)
                {
                    var logger = _loggerFactory.CreateLogger<MedicalAttachmentDALImpl>();
                    _medicalAttachmentRepository = new MedicalAttachmentDALImpl(_dbContext, logger);
                }
                return _medicalAttachmentRepository;
            }
        }

        public IMedicalEventDAL MedicalEventRepository
        {
            get
            {
                if (_medicalEventRepository == null)
                {
                    var logger = _loggerFactory.CreateLogger<MedicalEventDALImpl>();
                    _medicalEventRepository = new MedicalEventDALImpl(_dbContext, logger);
                }
                return _medicalEventRepository;
            }
        }

        public IMedicationDAL MedicationRepository
        {
            get
            {
                if (_medicationRepository == null)
                {
                    var logger = _loggerFactory.CreateLogger<MedicationDALImpl>();
                    _medicationRepository = new MedicationDALImpl(_dbContext, logger);
                }
                return _medicationRepository;
            }
        }

        public INeighborhoodDAL NeighborhoodRepository
        {
            get
            {
                if (_neighborhoodRepository == null)
                {
                    var logger = _loggerFactory.CreateLogger<NeighborhoodDALImpl>();
                    _neighborhoodRepository = new NeighborhoodDALImpl(_dbContext, logger);
                }
                return _neighborhoodRepository;
            }
        }

        public IPetPhotoDAL PetPhotoRepository
        {
            get
            {
                if (_petPhotoRepository == null)
                {
                    var logger = _loggerFactory.CreateLogger<PetPhotoDALImpl>();
                    _petPhotoRepository = new PetPhotoDALImpl(_dbContext, logger);
                }
                return _petPhotoRepository;
            }
        }

        public IPetDAL PetRepository
        {
            get
            {
                if (_petRepository == null)
                {
                    var logger = _loggerFactory.CreateLogger<PetDALImpl>();
                    _petRepository = new PetDALImpl(_dbContext, logger);
                }
                return _petRepository;
            }
        }

        public ISpecieDAL SpecieRepository
        {
            get
            {
                if (_specieRepository == null)
                {
                    var logger = _loggerFactory.CreateLogger<SpecieDALImpl>();
                    _specieRepository = new SpecieDALImpl(_dbContext, logger);
                }
                return _specieRepository;
            }
        }

        public IStateDAL StateRepository
        {
            get
            {
                if (_stateRepository == null)
                {
                    var logger = _loggerFactory.CreateLogger<StateDALImpl>();
                    _stateRepository = new StateDALImpl(_dbContext, logger);
                }
                return _stateRepository;
            }
        }

        public IUserDAL UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    var logger = _loggerFactory.CreateLogger<UserDALImpl>();
                    _userRepository = new UserDALImpl(_dbContext, logger);
                }
                return _userRepository;
            }
        }

        public IVaccineDAL VaccineRepository
        {
            get
            {
                if (_vaccineRepository == null)
                {
                    var logger = _loggerFactory.CreateLogger<VaccineDALImpl>();
                    _vaccineRepository = new VaccineDALImpl(_dbContext, logger);
                }
                return _vaccineRepository;
            }
        }

        public async Task<Response> Commit()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
                return ResponseFactory.CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ResponseFactory.CreateFailedResponse(ex);
            }
        }

        public async Task<Response> CommitForUser()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
                return ResponseFactory.CreateSuccessResponse();
            }
            catch (Exception ex)
            {
                return ErrorHandler.Handle(ex);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                _disposed = true;
            }
        }
    }
}