using BLL.Impl;
using BLL.Validation;
using DAL.Impl;
using System.Reflection;

namespace WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assemblyMarkers = new[]
            {
                typeof(AddressDALImpl),
                typeof(AddressServiceImpl),
                typeof(BreedDALImpl),
                typeof(BreedServiceImpl),
                typeof(CityDALImpl),
                typeof(CityServiceImpl),
                typeof(DiagnosisDALImpl),
                typeof(DiagnosisServiceImpl),
                typeof(DocumentAttachmentDALImpl),
                typeof(DocumentAttachmentServiceImpl),
                typeof(DocumentDALImpl),
                typeof(DocumentServiceImpl),
                typeof(ExamDALImpl),
                typeof(ExamServiceImpl),
                typeof(HashServiceImpl),
                typeof(MedicalAttachmentDALImpl),
                typeof(MedicalAttachmentServiceImpl),
                typeof(MedicalEventDALImpl),
                typeof(MedicalEventServiceImpl),
                typeof(MedicationDALImpl),
                typeof(MedicationServiceImpl),
                typeof(NeighborhoodDALImpl),
                typeof(NeighborhoodServiceImpl),
                typeof(PetDALImpl),
                typeof(PetPhotoDALImpl),
                typeof(PetPhotoServiceImpl),
                typeof(PetServiceImpl),
                typeof(SpecieDALImpl),
                typeof(SpecieServiceImpl),
                typeof(StateDALImpl),
                typeof(StateServiceImpl),
                typeof(UserDALImpl),
                typeof(UserServiceImpl),
                typeof(VaccineDALImpl),
                typeof(VaccineServiceImpl),

                typeof(AddressValidator),
                typeof(BreedValidator),
                typeof(CityValidator),
                typeof(DiagnosisValidator),
                typeof(DocumentAttachmentValidator),
                typeof(DocumentValidator),
                typeof(ExamValidator),
                typeof(FileValidator),
                typeof(MedicalAttachmentValidator),
                typeof(MedicalEventValidator),
                typeof(MedicationValidator),
                typeof(NeighborhoodValidator),
                typeof(PetPhotoValidator),
                typeof(PetValidator),
                typeof(SpecieValidator),
                typeof(UserValidator),
                typeof(VaccineValidator),
            };

            foreach (var marker in assemblyMarkers)
            {
                var assembly = Assembly.GetAssembly(marker);
                if (assembly == null) continue;

                RegisterValidators(services, assembly);
                RegisterServicesFromAssembly(services, assembly);
            }

            return services;
        }

        private static void RegisterServicesFromAssembly(IServiceCollection services, Assembly assembly)
        {
            var serviceTypes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .Where(t => t.Name.EndsWith("Service") ||
                           t.Name.EndsWith("ServiceImpl") ||
                           t.Name.EndsWith("DAL") ||
                           t.Name.EndsWith("DALImpl") ||
                           t.Name.EndsWith("Repository") ||
                           t.Name.EndsWith("RepositoryImpl"));

            foreach (var serviceType in serviceTypes)
            {
                var interfaceType = serviceType.GetInterfaces()
                    .FirstOrDefault(i =>
                        i.Name.Equals($"I{serviceType.Name}") ||
                        i.Name.Equals($"I{serviceType.Name.Replace("Impl", "")}"));

                if (interfaceType != null)
                {
                    services.AddScoped(interfaceType, serviceType);
                }
            }
        }

        private static void RegisterValidators(IServiceCollection services, Assembly assembly)
        {
            var validatorTypes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .Where(t => t.Name.EndsWith("Validator"));

            foreach (var validatorType in validatorTypes)
            {
                services.AddScoped(validatorType);
            }
        }
    }
}