using System.Reflection;

namespace WebApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assemblyMarkers = new[]
            {
                typeof(BLL.Impl.AddressServiceImpl),
                typeof(DAL.Impl.AddressDALImpl),
                typeof(BLL.Impl.BreedServiceImpl),
                typeof(DAL.Impl.BreedDALImpl),
                typeof(BLL.Impl.CityServiceImpl),
                typeof(DAL.Impl.CityDALImpl),
                typeof(BLL.Impl.HashServiceImpl),
                typeof(BLL.Impl.NeighborhoodServiceImpl),
                typeof(DAL.Impl.NeighborhoodDALImpl),
                typeof(BLL.Impl.PetServiceImpl),
                typeof(DAL.Impl.PetDALImpl),
                typeof(BLL.Impl.SpecieServiceImpl),
                typeof(DAL.Impl.SpecieDALImpl),
                typeof(BLL.Impl.StateServiceImpl),
                typeof(DAL.Impl.StateDALImpl),
                typeof(BLL.Impl.UserServiceImpl),
                typeof(DAL.Impl.UserDALImpl),
            };

            // Register validators from all assemblies
            foreach (var marker in assemblyMarkers)
            {
                var assembly = Assembly.GetAssembly(marker);
                if (assembly == null) continue;

                var validatorTypes = assembly.GetTypes()
                    .Where(t => t.IsClass && !t.IsAbstract)
                    .Where(t => t.Name.EndsWith("Validator"));

                foreach (var validatorType in validatorTypes)
                {
                    services.AddScoped(validatorType);
                }

                // Register other services
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
    }
}