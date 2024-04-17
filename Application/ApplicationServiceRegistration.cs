using Application.Behaviours;
using Application.MappingProfiles;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddMappingProfiles();

            services.RegisterFluentValidation();

            services.AddMemoryCache();

            return services;
        }
        private static void AddMappingProfiles(this IServiceCollection services)
        {
            var config = new MapperConfiguration(c =>
            {
                c.AddProfile<VisitProfile>();
                c.AddProfile<BrandProfile>();
                c.AddProfile<BranchProfile>();
                c.AddProfile<AreaProfile>();
                c.AddProfile<CustomServiceProfile>();
                c.AddProfile<BrandCostProfile>();
                c.AddProfile<ClientProfile>();
            });
            services.AddSingleton<IMapper>(s => config.CreateMapper());
        }

        private static void RegisterFluentValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        }
    }
}
