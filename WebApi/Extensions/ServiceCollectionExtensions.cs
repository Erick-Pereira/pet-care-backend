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
                typeof(AddressServiceImpl),
                typeof(AddressDALImpl),
                typeof(BreedServiceImpl),
                typeof(BreedDALImpl),
                typeof(CityServiceImpl),
                typeof(CityDALImpl),
                typeof( HashServiceImpl),
                typeof(NeighborhoodServiceImpl),
                typeof(NeighborhoodDALImpl),
                typeof(PetServiceImpl),
                typeof(PetDALImpl),
                typeof(SpecieServiceImpl),
                typeof(SpecieDALImpl),
                typeof(StateServiceImpl),
                typeof(StateDALImpl),
                typeof(UserServiceImpl),
                typeof(UserDALImpl),

                typeof(AddressValidator),
                typeof(BreedValidator),
                typeof(CityValidator),
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
    }
}